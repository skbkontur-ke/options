using Kontur.Extern.Options;
using NUnit.Framework;

namespace Kontur.Extern.Tests.Options.Extraction
{
    [TestFixture]
    internal class IsNone_Should
    {
        private static TestCaseData Create(Option<int> option, bool isNone)
        {
            return new TestCaseData(option).Returns(isNone);
        }

        private static readonly TestCaseData[] Cases =
        {
            Create(Option.None(), true),
            Create(10, false),
        };

        [TestCaseSource(nameof(Cases))]
        public bool Pass_HasValue(Option<int> option)
        {
            return option.IsNone;
        }
    }
}
