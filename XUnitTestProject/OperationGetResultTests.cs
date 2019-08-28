using Xunit;
using ConsoleApplication.Operations;

namespace XUnitTestProject
{
    /// <summary>
    /// ����� �� ��������� ���������� ��������� ��������.
    /// </summary>
    public class OperationGetResultTests
    {
        [Theory]
        [InlineData("", "<������� ���������� div>-<0>")]
        [InlineData("<dv>", "<������� ���������� div>-<0>")]
        [InlineData("<div>", "<������� ���������� div>-<1>")]
        [InlineData("1<div>a</div><div>b</div>abc<div>2", "<������� ���������� div>-<3>")]
        public void CalcDivCount(string content, string result)
        {

            var operation = new CalcDivCount();
            Assert.Equal(result, operation.GetResult(content));
        }

        [Theory]
        [InlineData("", "<������� ���������� ������ ����������>-<0>")]
        [InlineData("abc", "<������� ���������� ������ ����������>-<0>")]
        [InlineData(",", "<������� ���������� ������ ����������>-<1>")]
        [InlineData("a,b.c!def:ghi;j", "<������� ���������� ������ ����������>-<5>")]
        public void CalcPunctuationMarkCount(string content, string result)
        {
            var operation = new CalcPunctuationMarkCount();
            Assert.Equal(result, operation.GetResult(content));
        }

        [Theory]
        [InlineData("", "<��������, ��� ���������� \"{\" ��������� � ����������� \"}\">-<���������>")]
        [InlineData("{}", "<��������, ��� ���������� \"{\" ��������� � ����������� \"}\">-<���������>")]
        [InlineData("{abc}de{fg}h", "<��������, ��� ���������� \"{\" ��������� � ����������� \"}\">-<���������>")]
        [InlineData("{abc}de{fg}h}", "<��������, ��� ���������� \"{\" ��������� � ����������� \"}\">-<�� ���������>")]
        [InlineData("a{{abc}de{fg}h", "<��������, ��� ���������� \"{\" ��������� � ����������� \"}\">-<�� ���������>")]
        public void CheckBraces(string content, string result)
        {
            var operation = new CheckBraces();
            Assert.Equal(result, operation.GetResult(content));
        }
    }
}