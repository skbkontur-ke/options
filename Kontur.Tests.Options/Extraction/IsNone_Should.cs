using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Extraction
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
            Create(Option<int>.None(), true),
            Create(Option<int>.Some(10), false),
        };

        [TestCaseSource(nameof(Cases))]
        public bool Pass_HasValue(Option<int> option)
        {
            return option.IsNone;
        }
    }
}
