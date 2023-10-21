namespace Consumer.Consumers
{
    using Consumer.Interfaces;
    using MassTransit;
    using RabbitMqContracts;

    /// <summary>
    /// Подисчик на сообщение <see cref="SiteMsgModel"/>
    /// </summary>
    public class SiteConsumer : IConsumer<SiteMsgModel>
    {
        private readonly IRequestService _requestService;

        public SiteConsumer(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public async Task Consume(ConsumeContext<SiteMsgModel> context)
        {
            await _requestService.ExecuteAsync(context.Message.Url);
        }
    }
}