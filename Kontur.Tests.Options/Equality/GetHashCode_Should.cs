using FluentAssertions;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Equality
{
    [TestFixture]
    internal class GetHashCode_Should
    {
        private static TestCaseData Create<TValue1, TValue2>(Option<TValue1> option1, Option<TValue2> option2)
        {
            return new (option1, option2);
        }

        public static readonly TestCaseData[] Cases =
        {
            Create(Option.None<int>(), Option.None<int>()),
            Create(Option.None<string>(), Option.None<string>()),
            Create(Option.Some(500), Option.Some(500)),
        };

        [TestCaseSource(nameof(Cases))]
        public void Calculate(object option1, object option2)
        {
            var hashCode1 = option1.GetHashCode();
            var hashCode2 = option2.GetHashCode();

            hashCode1.Should().Be(hashCode2);
        }
    }
}