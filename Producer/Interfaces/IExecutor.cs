namespace Producer.Interfaces
{
    /// <summary>
    /// Исполнямый сервис.
    /// </summary>
    public interface IExecutor
    {
        /// <summary>
        /// Выполнение.
        /// </summary>
        Task ExecuteAsync(string[] args);
    }
}