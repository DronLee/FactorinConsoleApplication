using System.Text.RegularExpressions;

namespace ConsoleApplication.Operations
{
    /// <summary>
    /// Операция проверки на совпадение количества "{" с количеством "}".
    /// </summary>
    internal class CheckBraces : Operation
    {
        private const string _trueValue = "Совпадает";
        private const string _falseValue = "Не совпадает";
        private readonly Regex _regex1 = new Regex(@"\{", RegexOptions.Compiled);
        private readonly Regex _regex2 = new Regex(@"\}", RegexOptions.Compiled);

        public CheckBraces()
            : base("Проверка, что количество \"{\" совпадает с количеством \"}\"")
        { }

        protected override string Process(string content)
        {
            return _regex1.Matches(content).Count == _regex2.Matches(content).Count ?
                _trueValue : _falseValue;
        }
    }
}