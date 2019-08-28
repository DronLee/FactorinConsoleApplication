using Xunit;
using ConsoleApplication.Operations;

namespace XUnitTestProject
{
    /// <summary>
    /// Тесты на получение результата различных операций.
    /// </summary>
    public class OperationGetResultTests
    {
        [Theory]
        [InlineData("", "<Подсчёт количества div>-<0>")]
        [InlineData("<dv>", "<Подсчёт количества div>-<0>")]
        [InlineData("<div>", "<Подсчёт количества div>-<1>")]
        [InlineData("1<div>a</div><div>b</div>abc<div>2", "<Подсчёт количества div>-<3>")]
        public void CalcDivCount(string content, string result)
        {

            var operation = new CalcDivCount();
            Assert.Equal(result, operation.GetResult(content));
        }

        [Theory]
        [InlineData("", "<Подсчёт количества знаков препинания>-<0>")]
        [InlineData("abc", "<Подсчёт количества знаков препинания>-<0>")]
        [InlineData(",", "<Подсчёт количества знаков препинания>-<1>")]
        [InlineData("a,b.c!def:ghi;j", "<Подсчёт количества знаков препинания>-<5>")]
        public void CalcPunctuationMarkCount(string content, string result)
        {
            var operation = new CalcPunctuationMarkCount();
            Assert.Equal(result, operation.GetResult(content));
        }

        [Theory]
        [InlineData("", "<Проверка, что количество \"{\" совпадает с количеством \"}\">-<Совпадает>")]
        [InlineData("{}", "<Проверка, что количество \"{\" совпадает с количеством \"}\">-<Совпадает>")]
        [InlineData("{abc}de{fg}h", "<Проверка, что количество \"{\" совпадает с количеством \"}\">-<Совпадает>")]
        [InlineData("{abc}de{fg}h}", "<Проверка, что количество \"{\" совпадает с количеством \"}\">-<Не совпадает>")]
        [InlineData("a{{abc}de{fg}h", "<Проверка, что количество \"{\" совпадает с количеством \"}\">-<Не совпадает>")]
        public void CheckBraces(string content, string result)
        {
            var operation = new CheckBraces();
            Assert.Equal(result, operation.GetResult(content));
        }
    }
}