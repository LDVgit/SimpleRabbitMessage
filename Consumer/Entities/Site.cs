namespace Consumer.Entities
{
    /// <summary>
    /// Сайт с ответом
    /// </summary>
    public class Site
    {
        /// <summary> Идентификатор </summary>
        public long Id { get; set; }

        /// <summary> URL </summary>
        public string Url { get; set; }

        /// <summary> Дата запроса </summary>
        public DateTimeOffset Date { get; set; }

        /// <summary> Tело ответа с сайта </summary>
        public string Response { get; set; }
    }
}