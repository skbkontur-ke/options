using FluentAssertions;
using Kontur.Options;
using Kontur.Options.Unsafe;
using NUnit.Framework;

namespace Kontur.Tests.Options.Instantiation
{
    [TestFixture]
    internal class Create_Via_Generic_Should
    {
        private static TestCaseData CreateCreateCase(Option<int> option, bool hasSome)
        {
            return new TestCaseData(option).Returns(hasSome);
        }

        private static readonly TestCaseData[] CreateCases =
        {
            CreateCreateCase(Option<int>.None(), false),
            CreateCreateCase(Option<int>.Some(10), true),
        };

        [TestCaseSource(nameof(CreateCases))]
        public bool Pass_HasValue(Option<int> option)
        {
            return option.HasSome;
        }

        [Test]
        public void Pass_Value_To_Some()
        {
            const int expected = 10;
            var option = Option<int>.Some(expected);

            var result = option.GetOrThrow();

            result.Should().Be(expected);
        }
    }
}
