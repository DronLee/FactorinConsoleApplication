namespace ConsoleApplication.Operations
{
    /// <summary>
    /// Абстракция операции.
    /// </summary>
    internal abstract class Operation : IOperation
    {
        /// <summary>
        /// Шаблон результата.
        /// </summary>
        private const string _resultPattern = "<{0}>-<{1}>";
        /// <summary>
        /// Наименование операции (для вывода в результат).
        /// </summary>
        private readonly string _name;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="name">Наименование операции (для вывода в результат).</param>
        protected Operation(string name)
        {
            _name = name;
        }

        /// <summary>
        /// Обработка.
        /// </summary>
        /// <param name="content">Содержимое файла.</param>
        /// <returns>Результат выполнения операции.</returns>
        protected abstract string Process(string content);

        /// <summary>
        /// Получение результата.
        /// </summary>
        /// <param name="content">Содержимое файла.</param>
        /// <returns>Результат выполнения операции.</returns>
        public string GetResult(string content)
        {
            return string.Format(_resultPattern, _name, Process(content));
        }
    }
}