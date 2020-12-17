using FluentAssertions;
using Kontur.Options;
using Kontur.Options.Unsafe;
using NUnit.Framework;

namespace Kontur.Tests.Options.Instantiation.Create_Via_Non_Generic
{
    [TestFixture]
    internal class ReferenceType_Should
    {
        private static TestCaseData Create(Option<string> option, bool hasValue)
        {
            return Common.CreateHasValueCase(option, hasValue);
        }

        private static readonly TestCaseData[] Cases =
        {
            Create(Option.None<string>(), false),
            Create(Option.Some("foo"), true),
        };

        [TestCaseSource(nameof(Cases))]
        public bool Store_HasValue(Option<string> option)
        {
            return option.HasSome;
        }

        [Test]
        public void Store_Value()
        {
            const string expected = "foo";
            var option = Option.Some(expected);

            var result = option.GetOrThrow();

            result.Should().Be(expected);
        }
    }
}
