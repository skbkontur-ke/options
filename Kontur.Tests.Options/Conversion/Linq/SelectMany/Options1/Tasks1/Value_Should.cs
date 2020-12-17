using System.Collections.Generic;
using System.Threading.Tasks;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Conversion.Linq.SelectMany.Options1.Tasks1
{
    [TestFixture]
    internal class Value_Should
    {
        private static readonly Task<int> Task1000 = Common.Task1000;

        private static readonly IEnumerable<TestCaseData> Cases = Common.Cases;

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> Task_Option(Option<int> option)
        {
            return
                from x in Task1000
                from y in option
                select x + y;
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> Task_TaskOption(Option<int> option)
        {
            return
                from x in Task1000
                from y in Task.FromResult(option)
                select x + y;
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> Option_Task(Option<int> option)
        {
            return
                from x in option
                from y in Task1000
                select x + y;
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> TaskOption_Task(Option<int> option)
        {
            return
                from x in Task.FromResult(option)
                from y in Task1000
                select x + y;
        }
    }
}