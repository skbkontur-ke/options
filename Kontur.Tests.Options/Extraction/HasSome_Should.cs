using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Extraction
{
    [TestFixture]
    internal class HasSome_Should
    {
        private static TestCaseData Create(Option<int> option, bool hasSome)
        {
            return new TestCaseData(option).Returns(hasSome);
        }

        private static readonly TestCaseData[] Cases =
        {
            Create(Option<int>.None(), false),
            Create(Option<int>.Some(10), true),
        };

        [TestCaseSource(nameof(Cases))]
        public bool Pass_HasValue(Option<int> option)
        {
            return option.HasSome;
        }
    }
}
