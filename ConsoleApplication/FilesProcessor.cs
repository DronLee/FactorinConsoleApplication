using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace ConsoleApplication
{
    /// <summary>
    /// Класс реализует мониторинг новых файлов в директории и их обработку.
    /// </summary>
    internal class FilesProcessor : IFilesProcessor, IDisposable
    {
        /// <summary>
        /// Путь к файлу, в который будет записан результат.
        /// </summary>
        private string _resultFile = Path.Combine(Environment.CurrentDirectory, "Result.txt");
        /// <summary>
        /// Признак запуска мониторинга.
        /// </summary>
        private bool _running = false;
        /// <summary>
        /// Поток, в котором выполняется мониторинг и обработка файлов.
        /// </summary>
        private Thread _processThread = null;
        /// <summary>
        /// Обрабатываемые типы файлов.
        /// </summary>
        private IEnumerable<IFileType> _fileTypes;

        public FilesProcessor(IEnumerable<IFileType> fileTypes)
        {
            _fileTypes = fileTypes;
        }

        /// <summary>
        /// Запуск мониторинга.
        /// </summary>
        /// <param name="directory">Наблюдаемая директория.</param>
        public void Run(string directory)
        {
            _running = true;
            _processThread = new Thread(() => Process(directory));
            _processThread.Start();
        }

        /// <summary>
        /// Остановка мониторинга.
        /// </summary>
        public void Stop()
        {
            _running = false;
            if (_processThread != null)
            {
                _processThread.Join();
                _processThread = null;
            }
        }

        /// <summary>
        /// Мониторинг и обработка новых файлов.
        /// </summary>
        /// <param name="directory">Наблюдаемая директория.</param>
        private void Process(string directory)
        {
            List<string> oldFiles = Directory.GetFiles(directory).ToList();
            while (_running)
            {
                string[] nowFiles = Directory.GetFiles(directory);
                foreach (var nowFile in nowFiles)
                    if (_running && !oldFiles.Contains(nowFile))
                    {
                        string[] result;
                        if (_fileTypes.Any(f => f.Extension == Path.GetExtension(nowFile)))
                            result = _fileTypes.First(
                                f => f.Extension == Path.GetExtension(nowFile)).GetResult(nowFile);
                        else
                            result = _fileTypes.First(f => f.Extension == null).GetResult(nowFile);
                        File.AppendAllLines(_resultFile, result);
                        oldFiles.Add(nowFile);
                    }
                if (_running)
                    Thread.Sleep(500);
            }
        }

        /// <summary>
        /// Освобождение ресурсов.
        /// </summary>
        public void Dispose()
        {
            Stop();
        }
    }
}