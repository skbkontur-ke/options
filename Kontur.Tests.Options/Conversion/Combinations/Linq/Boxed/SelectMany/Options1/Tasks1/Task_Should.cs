using System.Collections.Generic;
using System.Threading.Tasks;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Conversion.Combinations.Linq.Boxed.SelectMany.Options1.Tasks1
{
    internal class Task_Should<TFixtureCase> : LinqTestBase<TFixtureCase>
        where TFixtureCase : IFixtureCase, new()
    {
        private const int TaskTerm = 1000;
        private static readonly Task<int> Task1000 = Task.FromResult(TaskTerm);

        private static readonly IEnumerable<TestCaseData> Cases = CreateSelectCases(1, sum => sum + TaskTerm);

        private static Task<Option<int>> SelectResult(int value)
        {
            return Task.FromResult(GetOption(value));
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> Task_Option(Option<int> option)
        {
            return
                from x in Task1000
                from y in option
                select SelectResult(x + y);
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> Task_TaskOption(Option<int> option)
        {
            return
                from x in Task1000
                from y in Task.FromResult(option)
                select SelectResult(x + y);
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> Option_Task(Option<int> option)
        {
            return
                from x in option
                from y in Task1000
                select SelectResult(x + y);
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> TaskOption_Task(Option<int> option)
        {
            return
                from x in Task.FromResult(option)
                from y in Task1000
                select SelectResult(x + y);
        }
    }
}