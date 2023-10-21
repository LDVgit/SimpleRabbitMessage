namespace Producer.Services
{
    using MassTransit;
    using Producer.Interfaces;
    using RabbitMqContracts;

    /// <inheritdoc />
    public class Executor : IExecutor
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public Executor(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task ExecuteAsync(string[] args)
        {
            var path = args[0];
            if (string.IsNullOrEmpty(path))
            {
                Console.WriteLine("Пожалуйста передайте полное имя файла как аргумент!");
                Console.ReadKey();
                return;
            }
            
            var rows = await File.ReadAllLinesAsync(path);

            var options = new ParallelOptions()
            {
                MaxDegreeOfParallelism = 20
            };

            await Parallel.ForEachAsync(rows, options, async (url, _) =>
            {
                if (string.IsNullOrEmpty(url))
                {
                    return;
                }
            
                await Send(url);
            });

            Console.WriteLine("Нажмите любую кнопу для выхода...");
            Console.ReadKey();
        }

        /// <summary>
        /// Публикация сообщения в очередь.
        /// </summary>
        private async Task Send(string url)
        {
            var msg = new SiteMsgModel(url);
            await _publishEndpoint.Publish(msg);
        }
    }
}