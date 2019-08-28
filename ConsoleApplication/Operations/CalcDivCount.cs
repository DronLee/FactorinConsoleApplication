using System.Text.RegularExpressions;

namespace ConsoleApplication.Operations
{
    /// <summary>
    /// Операция "Подсчёт количества dev".
    /// </summary>
    internal class CalcDivCount : Operation
    {
        private Regex _regex = new Regex(@"\<div\>", RegexOptions.Compiled);

        public CalcDivCount() : base("Подсчёт количества div") { }

        protected override string Process(string content)
        {
            return _regex.Matches(content).Count.ToString();
        }
    }
}