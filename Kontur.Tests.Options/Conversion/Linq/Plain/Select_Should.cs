using System.Collections.Generic;
using System.Threading.Tasks;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Conversion.Linq.Plain
{
    [TestFixture]
    internal class Select_Should
    {
        private static readonly IEnumerable<TestCaseData> Cases = SelectCasesGenerator.Create(1).ToTestCases();

        [TestCaseSource(nameof(Cases))]
        public Option<int> OneOption(Option<int> option)
        {
            return
                from value in option
                select value;
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> TaskOption(Option<int> option)
        {
            return
                from value in Task.FromResult(option)
                select value;
        }
    }
}
