namespace RabbitMqContracts
{
    /// <summary>
    /// Модель сообщения.
    /// </summary>
    public class SiteMsgModel
    {
        public SiteMsgModel(string url)
        {
            Url = url;
        }

        /// <summary> Url. </summary>
        public string Url { get; }
    }
}

