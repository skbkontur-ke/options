using FluentAssertions;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Extraction
{
    [TestFixture]
    internal class MatchOnNoneValue_Should
    {
        [Test]
        public void Return_OnSome()
        {
            const string expected = "foo";
            var option = Option.Some(expected);

            var result = option.Match("bar", x => x);

            result.Should().Be(expected);
        }

        [Test]
        public void Return_OnNone()
        {
            var option = Option.None<string>();
            const string expected = "bar";

            var result = option.Match(expected, _ => "foo");

            result.Should().Be(expected);
        }

        [Test]
        public void Do_Not_Call_OnSome_If_None()
        {
            var option = Option.None<string>();

            option.Match(string.Empty, _ => AssertDoNotCalled());
        }

        private static string AssertDoNotCalled()
        {
            Assert.Fail("OnSome called");
            return string.Empty;
        }
    }
}
