using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Conversion.Linq.Where.SelectMany
{
    [TestFixture]
    internal class Pass_Filter_Should
    {
        private static readonly IEnumerable<TestCaseData> Cases = Common.Cases.Select(testCase => new TestCaseData(testCase.Args).Returns(testCase.Result));

        [TestCaseSource(nameof(Cases))]
        public Option<int> Option_Option_Where(Option<int> option1, Option<int> option2)
        {
            return
                from x in option1
                from y in option2
                where x > 0
                select x + y;
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> TaskOption_Option_Where(Option<int> option1, Option<int> option2)
        {
            return
                from x in Task.FromResult(option1)
                from y in option2
                where x > 0
                select x + y;
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> Option_TaskOption_Where(Option<int> option1, Option<int> option2)
        {
            return
                from x in option1
                from y in Task.FromResult(option2)
                where x > 0
                select x + y;
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> TaskOption_TaskOption_Where(Option<int> option1, Option<int> option2)
        {
            return
                from x in Task.FromResult(option1)
                from y in Task.FromResult(option2)
                where x > 0
                select x + y;
        }

        [TestCaseSource(nameof(Cases))]
        public Option<int> Option_Where_Option(Option<int> option1, Option<int> option2)
        {
            return
                from x in option1
                where x > 0
                from y in option2
                select x + y;
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> TaskOption_Where_Option(Option<int> option1, Option<int> option2)
        {
            return
                from x in Task.FromResult(option1)
                where x > 0
                from y in option2
                select x + y;
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> Option_Where_TaskOption(Option<int> option1, Option<int> option2)
        {
            return
                from x in option1
                where x > 0
                from y in Task.FromResult(option2)
                select x + y;
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> TaskOption_Where_TaskOption(Option<int> option1, Option<int> option2)
        {
            return
                from x in Task.FromResult(option1)
                where x > 0
                from y in Task.FromResult(option2)
                select x + y;
        }
    }
}
