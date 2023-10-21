namespace Consumer
{
    using Consumer.Consumers;
    using Consumer.Infrastructure;
    using Consumer.Interfaces;
    using Consumer.Services;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using RabbitMqContracts;

    /// <summary>
    /// Класс работы с хостом.
    /// </summary>
    internal static class HostConfigure
    {
        /// <summary>
        /// Настройка хоста
        /// </summary>
        internal static IHost CreateHost()
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration(app =>
                {
                    app.AddJsonFile("local.settings.json", optional: true, reloadOnChange: true);
                })
                .ConfigureServices(ConfigureServices)
                .Build();

            host.MigrateDbContext<DataContext>();

            return host;
        }

        /// <summary>
        /// Добавление сервисов в контейнер
        /// </summary>
        private static void ConfigureServices(HostBuilderContext hostBuilderContext, IServiceCollection serviceCollection)
        {
            var configuration = hostBuilderContext.Configuration;
            serviceCollection.AddScoped<IRequestService, RequestService>();
            serviceCollection.AddHttpClient<IRequestService, RequestService>()
                .ConfigurePrimaryHttpMessageHandler(() =>
                {
                    return new HttpClientHandler()
                    {
                        ServerCertificateCustomValidationCallback = (message, certificate2, arg3, arg4) => true
                    };
                });

            serviceCollection.AddRabbitMqService(configuration, true, typeof(SiteConsumer));

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            serviceCollection.AddDbContext<DataContext>(builder => 
                builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
        }

        /// <summary>
        /// Выполнение миграции
        /// </summary>
        public static IHost MigrateDbContext<TContext>(this IHost host) where TContext : DbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetService<TContext>();
                context.Database.Migrate();
            }
            return host;
        }
    }
}

