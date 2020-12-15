using FluentAssertions;
using Kontur.Extern.Options;
using Kontur.Extern.Options.Unsafe;
using NUnit.Framework;

namespace Kontur.Extern.Tests.Options.Instantiation.Create_Via_Non_Generic
{
    [TestFixture]
    internal class ValueType_Should
    {
        private static TestCaseData Create(Option<int> option, bool hasValue)
        {
            return Common.CreateHasValueCase(option, hasValue);
        }

        private static readonly TestCaseData[] Cases =
        {
            Create(Option.None<int>(), false),
            Create(Option.Some(10), true),
        };

        [TestCaseSource(nameof(Cases))]
        public bool Store_HasValue(Option<int> option)
        {
            return option.HasSome;
        }

        [Test]
        public void Store_Value()
        {
            const int expected = 10;
            var option = Option.Some(expected);

            var result = option.GetOrThrow();

            result.Should().Be(expected);
        }
    }
}
