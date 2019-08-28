namespace ConsoleApplication
{
    public interface IFilesProcessor
    {
        /// <summary>
        /// Запуск мониторинга.
        /// </summary>
        /// <param name="directory">Наблюдаемая директория.</param>
        void Run(string directory);

        /// <summary>
        /// Остановка мониторинга.
        /// </summary>
        void Stop();
    }
}