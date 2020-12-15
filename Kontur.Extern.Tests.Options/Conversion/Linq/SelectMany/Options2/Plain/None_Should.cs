using System.Collections.Generic;
using System.Threading.Tasks;
using Kontur.Extern.Options;
using NUnit.Framework;

namespace Kontur.Extern.Tests.Options.Conversion.Linq.SelectMany.Options2.Plain
{
    [TestFixture]
    internal class None_Should
    {
        private static readonly Option<int> None = Option.None();

        private static readonly IEnumerable<TestCaseData> Cases = Options2Common.NoneCases;

        [TestCaseSource(nameof(Cases))]
        public Option<int> Option_Option(Option<int> option1, Option<int> option2)
        {
            return
                from x in option1
                from y in option2
                select None;
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> TaskOption_Option(Option<int> option1, Option<int> option2)
        {
            return
                from x in Task.FromResult(option1)
                from y in option2
                select None;
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> Option_TaskOption(Option<int> option1, Option<int> option2)
        {
            return
                from x in option1
                from y in Task.FromResult(option2)
                select None;
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> TaskOption_TaskOption(Option<int> option1, Option<int> option2)
        {
            return
                from x in Task.FromResult(option1)
                from y in Task.FromResult(option2)
                select None;
        }
    }
}
