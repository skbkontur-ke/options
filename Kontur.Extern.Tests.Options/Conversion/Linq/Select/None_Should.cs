using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kontur.Extern.Options;
using NUnit.Framework;

namespace Kontur.Extern.Tests.Options.Conversion.Linq.Select
{
    [TestFixture]
    internal class None_Should
    {
        private static readonly Option<int> None = Option.None();

        private static readonly IEnumerable<TestCaseData> NoneCases = Common.Cases
            .Select(testCase => new TestCaseData(testCase.Args).Returns(None));

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
