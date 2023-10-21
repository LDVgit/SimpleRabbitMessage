namespace Producer
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Producer.Interfaces;
    using Producer.Services;
    using RabbitMqContracts;

    /// <summary>
    /// Класс работы с хостом.
    /// </summary>
    internal static class HostConfigure
    {
        /// <summary>
        /// Настройка хоста
        /// </summary>
        internal static IHost CreateHost() {
            var host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration(app =>
                {
                    app.AddJsonFile("local.settings.json", optional: true, reloadOnChange: true);
                })
                .ConfigureServices(ConfigureServices)
                .Build();

            return host;
        }

        /// <summary>
        /// Добавление сервисов в контейнер
        /// </summary>
        private static void ConfigureServices(HostBuilderContext hostBuilderContext, IServiceCollection serviceCollection)
        {
            var configuration = hostBuilderContext.Configuration;

            serviceCollection.AddSingleton<IExecutor, Executor>();
            serviceCollection.Configure<MassTransitOptions>
                (configuration.GetSection("MassTransitOptions"));

            serviceCollection.AddRabbitMqService(configuration);
        }
    }
}

