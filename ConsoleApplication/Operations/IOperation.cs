namespace ConsoleApplication.Operations
{
    public interface IOperation
    {
        /// <summary>
        /// Получение результата.
        /// </summary>
        /// <param name="content">Содержимое файла.</param>
        /// <returns>Результат выполнения операции.</returns>
        string GetResult(string content);
    }
}