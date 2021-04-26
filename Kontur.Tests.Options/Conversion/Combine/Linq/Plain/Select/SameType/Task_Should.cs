using System.Collections.Generic;
using System.Threading.Tasks;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Conversion.Combine.Linq.Plain.Select.SameType
{
    [TestFixture]
    internal class Task_Should
    {
        private static readonly IEnumerable<TestCaseData> Cases = SelectCasesGenerator.Create(1).ToTestCases();

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> OneOption(Option<int> option)
        {
            return
                from value in option
                select Task.FromResult(value);
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> Option_Let(Option<int> option)
        {
            return
                from valueLet in option
                let value = valueLet
                select Task.FromResult(value);
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> TaskOption(Option<int> option)
        {
            return
                from value in Task.FromResult(option)
                select Task.FromResult(value);
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> TaskOption_Let(Option<int> option)
        {
            return
                from valueLet in Task.FromResult(option)
                let value = valueLet
                select Task.FromResult(value);
        }
    }
}
