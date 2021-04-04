using System.Collections.Generic;
using System.Threading.Tasks;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Conversion.Linq.Boxed
{
    internal class Select_Should<TFixtureCase> : LinqTestBase<TFixtureCase>
        where TFixtureCase : IFixtureCase, new()
    {
        private static readonly IEnumerable<TestCaseData> Cases = FixtureCase.CreateSelectCases(1);

        [TestCaseSource(nameof(Cases))]
        public Option<int> Option_Value(Option<int> option)
        {
            return
                from value in option
                select GetOption(value);
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> Option_Task(Option<int> option)
        {
            return
                from value in option
                select Task.FromResult(GetOption(value));
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> TaskOption_Value(Option<int> option)
        {
            return
                from value in Task.FromResult(option)
                select GetOption(value);
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> TaskOption_Task(Option<int> option)
        {
            return
                from value in Task.FromResult(option)
                select Task.FromResult(GetOption(value));
        }
    }
}
