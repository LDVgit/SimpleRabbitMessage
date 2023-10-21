namespace RabbitMqContracts
{
    using MassTransit;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Расширения для IServiceCollection
    /// </summary>
    public static class MassTransitExtensions
    {
        /// <summary>
        /// Подключение к RabbitMQ.
        /// </summary>
        public static IServiceCollection AddRabbitMqService(
            this IServiceCollection services,
            IConfiguration configuration,
            bool hasConsumers = false,
            Type consumerType = null)
        {
            var rabbitMqOption = configuration
                .GetSection("MassTransitOptions")
                .Get<MassTransitOptions>();

            if (rabbitMqOption?.VirtualHost == null || rabbitMqOption.Host == null)
                throw new ArgumentNullException("Отсутвует секция 'MassTransitOptions' в local.settings.json");

            services.AddMassTransit(cfg =>
            {
                cfg.UsingRabbitMq((context, config) => 
                {
                    config.Host(rabbitMqOption.Host, 
                        rabbitMqOption.VirtualHost, c =>
                        {
                            c.Username(rabbitMqOption.UserName);
                            c.Password(rabbitMqOption.Password);
                        });
                           
                    config.ConfigureEndpoints(context);
                });
                
                if (hasConsumers && consumerType is not null)
                    cfg.AddConsumers(consumerType.Assembly);
            });

            return services;
        }
    }
}

