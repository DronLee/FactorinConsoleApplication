using System.Text.RegularExpressions;

namespace ConsoleApplication.Operations
{
    /// <summary>
    /// Операция "Подсчёт количества знаков препинания".
    /// </summary>
    internal class CalcPunctuationMarkCount : Operation
    {
        private readonly Regex _regex = new Regex(@"[\.,;:!\?]", RegexOptions.Compiled);

        public CalcPunctuationMarkCount()
            : base("Подсчёт количества знаков препинания")
        { }

        protected override string Process(string content)
        {
            return _regex.Matches(content).Count.ToString();
        }
    }
}