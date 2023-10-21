namespace Consumer.Interfaces
{
    /// <summary>
    /// Сервис работы с URL из очереди.
    /// </summary>
    public interface IRequestService
    {
        /// <summary>
        /// Работа с сcылкой.
        /// </summary>
        Task ExecuteAsync(string url);
    }
}