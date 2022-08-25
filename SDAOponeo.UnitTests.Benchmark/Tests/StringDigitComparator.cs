using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using System.Text.RegularExpressions;

namespace SDAOponeo.UnitTests.Benchmark.Tests
{
    [SimpleJob(RunStrategy.ColdStart, targetCount: 1000)]
    [MinColumn, MaxColumn, MeanColumn, MedianColumn]
    public class StringDigitComparator
    {
        [Benchmark]
        [Arguments("3215532421213321321")]
        public bool IsStringDigitUsingLinq(string str)
        {
            return str.All(char.IsDigit);
        }

        [Benchmark]
        [Arguments("3215532421213321321")]
        public bool IsStringDigitUsingRegex(string str)
        {
            return Regex.IsMatch(str, @"^\d+$");
        }

        [Benchmark]
        [Arguments("3215532421213321321")]
        public bool IStringDigitUsingDoubleParse(string str)
        {
            bool isNum = Double.TryParse(Convert.ToString(str), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out double retNum);
            return isNum;
        }
    }
}
