using System.Collections.Generic;
using System.Threading.Tasks;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Conversion.Linq.Boxed.Select
{
    [TestFixture]
    internal class None_Should
    {
        private static readonly Option<int> None = Option.None();

        private static readonly IEnumerable<TestCaseData> NoneCases = SelectCasesGenerator.Create(1).ToTestCases(None);

        [TestCaseSource(nameof(NoneCases))]
        public Option<int> OneOption(Option<int> option)
        {
            return
                from value in option
                select None;
        }

        [TestCaseSource(nameof(NoneCases))]
        public Task<Option<int>> TaskOption(Option<int> option)
        {
            return
                from value in Task.FromResult(option)
                select None;
        }
    }
}
