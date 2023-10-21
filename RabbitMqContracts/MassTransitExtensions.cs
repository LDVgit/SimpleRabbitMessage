namespace RabbitMqContracts
{
    /// <summary>
    /// Опции подклоючения к RabbitMq через massTransit
    /// </summary>
    public class MassTransitOptions
    {
        /// <summary> Флаг 'в отладке' </summary>
        public bool? IsDebug { get; set; }

        /// <summary> Хост. </summary>
        public string Host { get; set; }
        
        /// <summary> Пароль. </summary>
        public string Password { get; set; }
        
        /// <summary> Виртуальный хост. </summary>
        public string VirtualHost { get; set; }
        
        /// <summary> Пользователь. </summary>
        public string UserName { get; set; }
        
        /// <summary> Порт. </summary>
        public string Port { get; set; }
    }
}

