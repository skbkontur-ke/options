using System.Collections.Generic;
using System.Threading.Tasks;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Conversion.Linq.Boxed.SelectMany.Options2.Plain
{
    internal class Task_Should<TFixtureCase> : LinqTestBase<TFixtureCase>
        where TFixtureCase : IFixtureCase, new()
    {
        private static readonly IEnumerable<TestCaseData> Cases = FixtureCase.CreateSelectCases(2);

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> Option_Option(Option<int> option1, Option<int> option2)
        {
            return
                from x in option1
                from y in option2
                select Task.FromResult(GetOption(x + y));
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> Option_Let_Option(Option<int> option1, Option<int> option2)
        {
            return
                from xLet in option1
                let x = xLet
                from y in option2
                select Task.FromResult(GetOption(x + y));
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> Option_Option_Let(Option<int> option1, Option<int> option2)
        {
            return
                from x in option1
                from yLet in option2
                let y = yLet
                select Task.FromResult(GetOption(x + y));
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> Option_Let_Option_Let(Option<int> option1, Option<int> option2)
        {
            return
                from xLet in option1
                let x = xLet
                from yLet in option2
                let y = yLet
                select Task.FromResult(GetOption(x + y));
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> TaskOption_Option(Option<int> option1, Option<int> option2)
        {
            return
                from x in Task.FromResult(option1)
                from y in option2
                select Task.FromResult(GetOption(x + y));
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> TaskOption_Let_Option(Option<int> option1, Option<int> option2)
        {
            return
                from xLet in Task.FromResult(option1)
                let x = xLet
                from y in option2
                select Task.FromResult(GetOption(x + y));
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> TaskOption_Option_Let(Option<int> option1, Option<int> option2)
        {
            return
                from x in Task.FromResult(option1)
                from yLet in option2
                let y = yLet
                select Task.FromResult(GetOption(x + y));
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> TaskOption_Let_Option_Let(Option<int> option1, Option<int> option2)
        {
            return
                from xLet in Task.FromResult(option1)
                let x = xLet
                from yLet in option2
                let y = yLet
                select Task.FromResult(GetOption(x + y));
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> Option_TaskOption(Option<int> option1, Option<int> option2)
        {
            return
                from x in option1
                from y in Task.FromResult(option2)
                select Task.FromResult(GetOption(x + y));
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> Option_Let_TaskOption(Option<int> option1, Option<int> option2)
        {
            return
                from xLet in option1
                let x = xLet
                from y in Task.FromResult(option2)
                select Task.FromResult(GetOption(x + y));
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> Option_TaskOption_Let(Option<int> option1, Option<int> option2)
        {
            return
                from x in option1
                from yLet in Task.FromResult(option2)
                let y = yLet
                select Task.FromResult(GetOption(x + y));
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> Option_Let_TaskOption_Let(Option<int> option1, Option<int> option2)
        {
            return
                from xLet in option1
                let x = xLet
                from yLet in Task.FromResult(option2)
                let y = yLet
                select Task.FromResult(GetOption(x + y));
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> TaskOption_TaskOption(Option<int> option1, Option<int> option2)
        {
            return
                from x in Task.FromResult(option1)
                from y in Task.FromResult(option2)
                select Task.FromResult(GetOption(x + y));
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> TaskOption_Let_TaskOption(Option<int> option1, Option<int> option2)
        {
            return
                from xLet in Task.FromResult(option1)
                let x = xLet
                from y in Task.FromResult(option2)
                select Task.FromResult(GetOption(x + y));
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> TaskOption_TaskOption_Let(Option<int> option1, Option<int> option2)
        {
            return
                from x in Task.FromResult(option1)
                from yLet in Task.FromResult(option2)
                let y = yLet
                select Task.FromResult(GetOption(x + y));
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> TaskOption_Let_TaskOption_Let(Option<int> option1, Option<int> option2)
        {
            return
                from xLet in Task.FromResult(option1)
                let x = xLet
                from yLet in Task.FromResult(option2)
                let y = yLet
                select Task.FromResult(GetOption(x + y));
        }
    }
}
