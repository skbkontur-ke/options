using Kontur.Extern.Options;
using NUnit.Framework;

namespace Kontur.Extern.Tests.Options.Instantiation
{
    [TestFixture]
    internal class TernaryOperator_Should
    {
        private const int SomeValue = 7;

        private static TestCaseData Create(bool flag, Option<int> result)
        {
            return new TestCaseData(flag).Returns(result);
        }

        private static readonly TestCaseData[] Cases =
        {
            Create(true, SomeValue),
            Create(false, Option.None()),
        };

        [TestCaseSource(nameof(Cases))]
        public Option<int> Create(bool flag)
        {
            return flag
                ? Option.Some(SomeValue)
                : Option.None();
        }
    }
}
