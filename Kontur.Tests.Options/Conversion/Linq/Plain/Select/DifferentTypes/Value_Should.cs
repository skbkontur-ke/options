using System.Collections.Generic;
using System.Threading.Tasks;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Conversion.Linq.Plain.Select.DifferentTypes
{
    [TestFixture]
    internal class Value_Should
    {
        private static readonly IEnumerable<TestCaseData> Cases = SelectCasesGenerator.Create(1).ToDifferentTypeTestCases();

        private static string SelectResult(int value)
        {
            return SelectCaseToDifferentTypeTestCasesExtensions.ConvertToString(value);
        }

        [TestCaseSource(nameof(Cases))]
        public Option<string> OneOption(Option<int> option)
        {
            return
                from value in option
                select SelectResult(value);
        }

        [TestCaseSource(nameof(Cases))]
        public Option<string> Option_Let(Option<int> option)
        {
            return
                from valueLet in option
                let value = valueLet
                select SelectResult(value);
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<string>> TaskOption(Option<int> option)
        {
            return
                from value in Task.FromResult(option)
                select SelectResult(value);
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<string>> TaskOption_Let(Option<int> option)
        {
            return
                from valueLet in Task.FromResult(option)
                let value = valueLet
                select SelectResult(value);
        }
    }
}
