using System.Collections.Generic;
using System.Linq;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Conversion
{
    [TestFixture]
    internal class Upcast_Should
    {
        private static IEnumerable<TestCaseData> GetCases() =>
            UpcastExamples
                .Get()
                .Select(example => new TestCaseData(example.Option).Returns(example.Result));

        [TestCaseSource(nameof(GetCases))]
        public Option<Base> Process_Option(Option<Child> option)
        {
            return option.Upcast<Base>();
        }
    }
}
