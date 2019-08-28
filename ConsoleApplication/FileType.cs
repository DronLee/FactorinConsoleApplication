using System.IO;
using System.Linq;
using System.Text;
using ConsoleApplication.Operations;

namespace ConsoleApplication
{
    /// <summary>
    /// Абстракция обрабатываемого файла.
    /// </summary>
    internal class FileType : IFileType
    {
        /// <summary>
        /// Шаблон результата ответа.
        /// </summary>
        private const string _resultPattern = "<{0}>-{1}";
        /// <summary>
        /// Ожидаемая кодировка файла.
        /// </summary>
        private readonly Encoding _encoding = Encoding.UTF8;
        /// <summary>
        /// Операции, выполянемые над файлом.
        /// </summary>
        private readonly IOperation[] _operations;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="operations">Операции, выполянемые над файлом.</param>
        public FileType(string extension, IOperation[] operations)
        {
            Extension = extension;
            _operations = operations;
        }

        /// <summary>
        /// Расширение файла.
        /// </summary>
        public string Extension { get; }

        /// <summary>
        /// Получение результатов обработки файла (на каждую операцию свой результат).
        /// </summary>
        /// <param name="filePath">Путь к обрабатываемому файлу.</param>
        /// <returns>Результаты обработки файла.</returns>
        public string[] GetResult(string filePath)
        {
            var fileName = Path.GetFileName(filePath);
            var content = File.ReadAllText(filePath, _encoding);
            return _operations.Select(o => string.Format(_resultPattern, fileName, o.GetResult(content))).ToArray();
        }
    }
}