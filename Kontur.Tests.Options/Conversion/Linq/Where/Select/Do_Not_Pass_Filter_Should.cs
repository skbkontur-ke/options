using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Conversion.Linq.Where.Select
{
    [TestFixture]
    internal class Do_Not_Pass_Filter_Should
    {
        private static readonly IEnumerable<TestCaseData> Cases = Common.Cases
            .Select(testCase => new TestCaseData(testCase.Args).Returns(Option.None<int>()));

        [TestCaseSource(nameof(Cases))]
        public Option<int> OneOption(Option<int> option)
        {
            return
                from value in option
                where value < 0
                select value;
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> TaskOption(Option<int> option)
        {
            return
                from value in Task.FromResult(option)
                where value < 0
                select value;
        }
    }
}
