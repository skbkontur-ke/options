using System.Collections.Generic;
using System.Threading.Tasks;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Conversion.Linq.Plain.Where.Select
{
    [TestFixture]
    internal class Do_Not_Pass_Filter_Should
    {
        private static readonly IEnumerable<TestCaseData> Cases = SelectCasesGenerator.Create(1).ToNoneTestCases();

        private static bool Check(int value)
        {
            return value < 0;
        }

        [TestCaseSource(nameof(Cases))]
        public Option<int> OneOption(Option<int> option)
        {
            return
                from value in option
                where Check(value)
                select value;
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> TaskOption(Option<int> option)
        {
            return
                from value in Task.FromResult(option)
                where Check(value)
                select value;
        }
    }
}
