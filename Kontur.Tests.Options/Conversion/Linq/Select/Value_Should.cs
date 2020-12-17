using System.Collections.Generic;
using System.Threading.Tasks;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Conversion.Linq.Select
{
    [TestFixture]
    internal class Value_Should
    {
        private static readonly IEnumerable<TestCaseData> Cases = Common.ResultCases;

        [TestCaseSource(nameof(Cases))]
        public Option<int> Option_Value(Option<int> option)
        {
            return
                from value in option
                select value;
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> TaskOption_Value(Option<int> option)
        {
            return
                from value in Task.FromResult(option)
                select value;
        }
    }
}
