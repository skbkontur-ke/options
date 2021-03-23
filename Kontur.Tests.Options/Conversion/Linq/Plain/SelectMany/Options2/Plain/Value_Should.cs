using System.Collections.Generic;
using System.Threading.Tasks;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Conversion.Linq.Plain.SelectMany.Options2.Plain
{
    [TestFixture]
    internal class Value_Should
    {
        private static readonly IEnumerable<TestCaseData> Cases = SelectCasesGenerator.Create(2).ToTestCases();

        [TestCaseSource(nameof(Cases))]
        public Option<int> Option_Option(Option<int> option1, Option<int> option2)
        {
            return
                from x in option1
                from y in option2
                select x + y;
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> TaskOption_Option(Option<int> option1, Option<int> option2)
        {
            return
                from x in Task.FromResult(option1)
                from y in option2
                select x + y;
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> Option_TaskOption(Option<int> option1, Option<int> option2)
        {
            return
                from x in option1
                from y in Task.FromResult(option2)
                select x + y;
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> TaskOption_TaskOption(Option<int> option1, Option<int> option2)
        {
            return
                from x in Task.FromResult(option1)
                from y in Task.FromResult(option2)
                select x + y;
        }
    }
}
