using System.Collections.Generic;
using System.Threading.Tasks;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Conversion.Combinations.Linq.Boxed.Select.SameType
{
    internal class Value_Should<TFixtureCase> : LinqTestBase<TFixtureCase>
        where TFixtureCase : IFixtureCase, new()
    {
        private static readonly IEnumerable<TestCaseData> Cases = CreateSelectCases(1);

        [TestCaseSource(nameof(Cases))]
        public Option<int> OneOption(Option<int> option)
        {
            return
                from value in option
                select GetOption(value);
        }

        [TestCaseSource(nameof(Cases))]
        public Option<int> Option_Let(Option<int> option)
        {
            return
                from valueLet in option
                let value = valueLet
                select GetOption(value);
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> TaskOption(Option<int> option)
        {
            return
                from value in Task.FromResult(option)
                select GetOption(value);
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> TaskOption_Let(Option<int> option)
        {
            return
                from valueLet in Task.FromResult(option)
                let value = valueLet
                select GetOption(value);
        }
    }
}
