using System.Collections.Generic;
using System.Threading.Tasks;
using Kontur.Extern.Options;
using NUnit.Framework;

namespace Kontur.Extern.Tests.Options.Conversion.Linq.SelectMany.Options3
{
    [TestFixture]
    internal class Value_Should
    {
        private static readonly IEnumerable<TestCaseData> Cases = Common.ResultCases;

        [TestCaseSource(nameof(Cases))]
        public Option<int> Option_Option_Option(Option<int> option1, Option<int> option2, Option<int> option3)
        {
            return
                from x in option1
                from y in option2
                from z in option3
                select x + y + z;
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> TaskOption_Option_Option(Option<int> option1, Option<int> option2, Option<int> option3)
        {
            return
                from x in Task.FromResult(option1)
                from y in option2
                from z in option3
                select x + y + z;
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> Option_TaskOption_Option(Option<int> option1, Option<int> option2, Option<int> option3)
        {
            return
                from x in option1
                from y in Task.FromResult(option2)
                from z in option3
                select x + y + z;
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> Option_Option_TaskOption(Option<int> option1, Option<int> option2, Option<int> option3)
        {
            return
                from x in option1
                from y in option2
                from z in Task.FromResult(option3)
                select x + y + z;
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> TaskOption_TaskOption_Option(Option<int> option1, Option<int> option2, Option<int> option3)
        {
            return
                from x in Task.FromResult(option1)
                from y in Task.FromResult(option2)
                from z in option3
                select x + y + z;
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> TaskOption_Option_TaskOption(Option<int> option1, Option<int> option2, Option<int> option3)
        {
            return
                from x in Task.FromResult(option1)
                from y in option2
                from z in Task.FromResult(option3)
                select x + y + z;
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> Option_TaskOption_TaskOption(Option<int> option1, Option<int> option2, Option<int> option3)
        {
            return
                from x in option1
                from y in Task.FromResult(option2)
                from z in Task.FromResult(option3)
                select x + y + z;
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> TaskOption_TaskOption_TaskOption(Option<int> option1, Option<int> option2, Option<int> option3)
        {
            return
                from x in Task.FromResult(option1)
                from y in Task.FromResult(option2)
                from z in Task.FromResult(option3)
                select x + y + z;
        }
    }
}
