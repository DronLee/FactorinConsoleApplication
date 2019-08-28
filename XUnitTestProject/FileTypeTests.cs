using ConsoleApplication;
using ConsoleApplication.Operations;
using FakeItEasy;
using System;
using System.IO;
using Xunit;

namespace XUnitTestProject
{
    /// <summary>
    /// Тесты для проверки работы FileType.
    /// </summary>
    public class FileTypeTests
    {
        /// <summary>
        /// Проверка получения результата выполнения операций.
        /// </summary>
        [Fact]
        public void GetResult()
        {
            const string operation1Result = "<Операция1>-<Результат1>";
            const string operation2Result = "<Операция2>-<Результат2>";

            string comingContent = null;
            string[] result;

            var fileName = Guid.NewGuid().ToString();
            var filePath = Path.Combine(Path.GetTempPath(), fileName);
            const string fileContent = "TestData";

            var operation1 = A.Fake<IOperation>();
            A.CallTo(() => operation1.GetResult(A<string>.Ignored))
                // Чтобы можно было проверить, что в операцию поступает нужные данные.
                .Invokes((string content) => { comingContent = content; })
                .Returns(operation1Result);

            var operation2 = A.Fake<IOperation>();
            A.CallTo(() => operation2.GetResult(A<string>.Ignored)).Returns(operation2Result);

            var fileType = new FileType(null, new[] { operation1, operation2 });

            File.WriteAllText(filePath, fileContent);
            try
            {
                result = fileType.GetResult(filePath);
            }
            finally
            {
                File.Delete(filePath);
            }

            Assert.Equal(2, result.Length);
            Assert.Equal(fileContent, comingContent);
            Assert.Equal($"<{fileName}>-{operation1Result}", result[0]);
            Assert.Equal($"<{fileName}>-{operation2Result}", result[1]);
        }
    }
}