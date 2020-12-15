using FluentAssertions;
using Kontur.Extern.Options;
using NUnit.Framework;

namespace Kontur.Extern.Tests.Options.Extraction
{
    [TestFixture]
    internal class MatchOnNoneValue_Should
    {
        [Test]
        public void Return_OnSome()
        {
            const string expected = "foo";
            var option = Option.Some(expected);

            var result = option.Match(x => x, "bar");

            result.Should().Be(expected);
        }

        [Test]
        public void Return_OnNone()
        {
            var option = Option.None<string>();
            const string expected = "bar";

            var result = option.Match(_ => "foo", expected);

            result.Should().Be(expected);
        }

        [Test]
        public void Do_Not_Call_OnSome_If_None()
        {
            var option = Option.None<string>();

            option.Match(
                _ => AssertDoNotCalled(),
                string.Empty);
        }

        private static string AssertDoNotCalled()
        {
            Assert.Fail("OnSome called");
            return string.Empty;
        }
    }
}
