using ConsoleApplication;
using FakeItEasy;
using System;
using System.IO;
using System.Threading;
using Xunit;

namespace XUnitTestProject
{
    /// <summary>
    /// Тесты для проверки работы FilesProcessor.
    /// </summary>
    public class FilesProcessorTests
    {
        /// <summary>
        /// Проверка обработки файлов.
        /// </summary>
        [Fact]
        public void Process()
        {
            string processedEmptyFile = null;
            string processedTxtFile = null;

            const string emptyFileName = "File";
            const string txtFileName = "File.txt";

            const string emptyFileTypeResult = "EmptyFileTypeResult";
            const string txtFileTypeResult = "TxtFileTypeResult";

            var resultFile = Path.Combine(Environment.CurrentDirectory, "Result.txt");

            var directory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(directory);
            try
            {
                File.WriteAllBytes(Path.Combine(directory, "OldFile"), new byte[0]);

                var emptyFileType = A.Fake<IFileType>();
                A.CallTo(() => emptyFileType.Extension).Returns(null);
                A.CallTo(() => emptyFileType.GetResult(A<string>.Ignored))
                    .Invokes((string filePath) => { processedEmptyFile = filePath; })
                    .Returns(new[] { emptyFileTypeResult });

                var txtFileType = A.Fake<IFileType>();
                A.CallTo(() => txtFileType.Extension).Returns(".txt");
                A.CallTo(() => txtFileType.GetResult(A<string>.Ignored))
                    .Invokes((string filePath) => { processedTxtFile = filePath; })
                    .Returns(new[] { txtFileTypeResult });

                if (File.Exists(resultFile))
                    File.Delete(resultFile);

                using (var filesProcessor = new FilesProcessor(new[] { emptyFileType, txtFileType }))
                {
                    filesProcessor.Run(directory);

                    Thread.Sleep(500); // Чтобы успел запуститься.

                    File.WriteAllBytes(Path.Combine(directory, txtFileName), new byte[0]);
                    File.WriteAllBytes(Path.Combine(directory, emptyFileName), new byte[0]);

                    Thread.Sleep(500); // Чтобы успел обработать.
                }
            }
            finally
            {
                Directory.Delete(directory, true);
            }

            Assert.True(File.Exists(resultFile));

            var resultLines = File.ReadAllLines(resultFile);
            Assert.Equal(2, resultLines.Length);
            Assert.Contains(emptyFileTypeResult, resultLines);
            Assert.Contains(txtFileTypeResult, resultLines);
        }

        [Fact]
        public void Stop()
        {
            var fileIsProcessed = false;

            var txtFileType = A.Fake<IFileType>();
            A.CallTo(() => txtFileType.Extension).Returns(".txt");
            A.CallTo(() => txtFileType.GetResult(A<string>.Ignored))
                .Invokes(() => { fileIsProcessed = true; })
                .Returns(new string[0]);

            var resultFile = Path.Combine(Environment.CurrentDirectory, "Result.txt");
            if (File.Exists(resultFile))
                File.Delete(resultFile);

            var directory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(directory);
            try
            {
                var filesProcessor = new FilesProcessor(new[] { txtFileType });
                filesProcessor.Run(directory);

                Thread.Sleep(500); // Чтобы успел запуститься.

                filesProcessor.Stop();

                File.WriteAllBytes(Path.Combine(directory, "File1.txt"), new byte[0]);
                File.WriteAllBytes(Path.Combine(directory, "File2.txt"), new byte[0]);

                Thread.Sleep(500); // Чтобы успел обработать, если что.
            }
            finally
            {
                Directory.Delete(directory, true);
            }

            // Файлы не должны были обрабатываться, так как появлялись после остановки.
            Assert.False(fileIsProcessed);
            Assert.False(File.Exists(resultFile));
        }
    }
}