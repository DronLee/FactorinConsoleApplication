namespace ConsoleApplication
{
    public interface IFileType
    {
        /// <summary>
        /// Расширение файла.
        /// </summary>
        string Extension { get; }

        /// <summary>
        /// Получение результатов обработки файла (на каждую операцию свой результат).
        /// </summary>
        /// <param name="filePath">Путь к обрабатываемому файлу.</param>
        /// <returns>Результаты обработки файла.</returns>
        string[] GetResult(string filePath);
    }
}