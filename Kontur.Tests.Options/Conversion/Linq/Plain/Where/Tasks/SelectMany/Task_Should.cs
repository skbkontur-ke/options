using System.Collections.Generic;
using System.Threading.Tasks;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Conversion.Linq.Plain.Where.Tasks.SelectMany
{
    [TestFixture]
    internal class Task_Should
    {
        private static readonly IEnumerable<TestCaseData> Cases = WhereCaseGenerator.Create(2);

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> Option_Option_Where(Option<int> option1, Option<int> option2, IsSuitable isSuitable)
        {
            return
                from x in option1
                from y in option2
                where Task.FromResult(isSuitable(x))
                select Task.FromResult(x + y);
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> TaskOption_Option_Where(Option<int> option1, Option<int> option2, IsSuitable isSuitable)
        {
            return
                from x in Task.FromResult(option1)
                from y in option2
                where Task.FromResult(isSuitable(x))
                select Task.FromResult(x + y);
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> Option_TaskOption_Where(Option<int> option1, Option<int> option2, IsSuitable isSuitable)
        {
            return
                from x in option1
                from y in Task.FromResult(option2)
                where Task.FromResult(isSuitable(x))
                select Task.FromResult(x + y);
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> TaskOption_TaskOption_Where(Option<int> option1, Option<int> option2, IsSuitable isSuitable)
        {
            return
                from x in Task.FromResult(option1)
                from y in Task.FromResult(option2)
                where Task.FromResult(isSuitable(x))
                select Task.FromResult(x + y);
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> Option_Where_Option(Option<int> option1, Option<int> option2, IsSuitable isSuitable)
        {
            return
                from x in option1
                where Task.FromResult(isSuitable(x))
                from y in option2
                select Task.FromResult(x + y);
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> TaskOption_Where_Option(Option<int> option1, Option<int> option2, IsSuitable isSuitable)
        {
            return
                from x in Task.FromResult(option1)
                where Task.FromResult(isSuitable(x))
                from y in option2
                select Task.FromResult(x + y);
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> Option_Where_TaskOption(Option<int> option1, Option<int> option2, IsSuitable isSuitable)
        {
            return
                from x in option1
                where Task.FromResult(isSuitable(x))
                from y in Task.FromResult(option2)
                select Task.FromResult(x + y);
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> TaskOption_Where_TaskOption(Option<int> option1, Option<int> option2, IsSuitable isSuitable)
        {
            return
                from x in Task.FromResult(option1)
                where Task.FromResult(isSuitable(x))
                from y in Task.FromResult(option2)
                select Task.FromResult(x + y);
        }
    }
}
