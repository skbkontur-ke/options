using FluentAssertions;
using Kontur.Extern.Options;
using NUnit.Framework;

namespace Kontur.Extern.Tests.Options.Equality
{
    [TestFixture]
    internal class GetHashCode_Should
    {
        private static TestCaseData CreateCase<TValue1, TValue2>(Option<TValue1> option1, Option<TValue2> option2)
        {
            return new TestCaseData(option1, option2);
        }

        public static readonly TestCaseData[] Cases =
        {
            CreateCase(Option.None<int>(), Option.None<int>()),
            CreateCase(Option.None<string>(), Option.None<string>()),
            CreateCase(Option.Some(500), Option.Some(500)),
        };

        [TestCaseSource(nameof(Cases))]
        public void Calculate(object options1, object options2)
        {
            var hashCode1 = options1.GetHashCode();
            var hashCode2 = options2.GetHashCode();

            hashCode1.Should().Be(hashCode2);
        }
    }
}