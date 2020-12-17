using FluentAssertions;
using Kontur.Options;
using Kontur.Options.Unsafe;
using NUnit.Framework;

namespace Kontur.Tests.Options.Instantiation
{
    [TestFixture]
    internal class Implicit_Operator_Should
    {
        [Test]
        public void Create_Some()
        {
            const int expected = 10;

            Option<int> option = expected;

            var result = option.GetOrThrow();
            result.Should().Be(expected);
        }

        [Test]
        public void Create_None()
        {
            Option<int> option = Option.None();

            option.HasSome.Should().Be(false);
        }
    }
}
