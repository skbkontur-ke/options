using System.Collections.Generic;
using System.Threading.Tasks;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Conversion.Combinations.Linq.Plain.Where.Plain.Select
{
    [TestFixture]
    internal class Task_Should
    {
        private static readonly IEnumerable<TestCaseData> Cases = WhereCaseGenerator.Create(1);

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> OneOption(Option<int> option, IsSuitable isSuitable)
        {
            return
                from value in option
                where isSuitable(value)
                select Task.FromResult(value);
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> TaskOption(Option<int> option, IsSuitable isSuitable)
        {
            return
                from value in Task.FromResult(option)
                where isSuitable(value)
                select Task.FromResult(value);
        }
    }
}
