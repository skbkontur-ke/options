using System.Collections.Generic;
using System.Threading.Tasks;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Conversion.Combinations.Linq.Boxed.Where.Tasks.SelectMany
{
    internal class Value_Should<TFixtureCase> : LinqTestBase<TFixtureCase>
        where TFixtureCase : IFixtureCase, new()
    {
        private static readonly IEnumerable<TestCaseData> Cases = FixtureCase.CreateWhereCases(Constant, 2);

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> Option_Option_Where(Option<int> option1, Option<int> option2, IsSuitable isSuitable)
        {
            return
                from x in option1
                from y in option2
                where Task.FromResult(isSuitable(x))
                select GetOption(x + y);
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> TaskOption_Option_Where(Option<int> option1, Option<int> option2, IsSuitable isSuitable)
        {
            return
                from x in Task.FromResult(option1)
                from y in option2
                where Task.FromResult(isSuitable(x))
                select GetOption(x + y);
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> Option_TaskOption_Where(Option<int> option1, Option<int> option2, IsSuitable isSuitable)
        {
            return
                from x in option1
                from y in Task.FromResult(option2)
                where Task.FromResult(isSuitable(x))
                select GetOption(x + y);
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> TaskOption_TaskOption_Where(Option<int> option1, Option<int> option2, IsSuitable isSuitable)
        {
            return
                from x in Task.FromResult(option1)
                from y in Task.FromResult(option2)
                where Task.FromResult(isSuitable(x))
                select GetOption(x + y);
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> Option_Where_Option(Option<int> option1, Option<int> option2, IsSuitable isSuitable)
        {
            return
                from x in option1
                where Task.FromResult(isSuitable(x))
                from y in option2
                select GetOption(x + y);
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> TaskOption_Where_Option(Option<int> option1, Option<int> option2, IsSuitable isSuitable)
        {
            return
                from x in Task.FromResult(option1)
                where Task.FromResult(isSuitable(x))
                from y in option2
                select GetOption(x + y);
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> Option_Where_TaskOption(Option<int> option1, Option<int> option2, IsSuitable isSuitable)
        {
            return
                from x in option1
                where Task.FromResult(isSuitable(x))
                from y in Task.FromResult(option2)
                select GetOption(x + y);
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> TaskOption_Where_TaskOption(Option<int> option1, Option<int> option2, IsSuitable isSuitable)
        {
            return
                from x in Task.FromResult(option1)
                where Task.FromResult(isSuitable(x))
                from y in Task.FromResult(option2)
                select GetOption(x + y);
        }
    }
}
