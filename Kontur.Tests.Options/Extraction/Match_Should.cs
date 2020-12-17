using FluentAssertions;
using JetBrains.Annotations;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Extraction
{
    [TestFixture]
    internal class Match_Should
    {
        [Test]
        public void Return_OnSome()
        {
            const string expectedPrefix = "foo";
            var option = Option.Some(expectedPrefix);
            const string expectedSuffix = "processed";

            var result = option.Match(() => "bar", val => val + expectedSuffix);

            result.Should().Be(expectedPrefix + expectedSuffix);
        }

        [Test]
        public void Return_OnNone()
        {
            var option = Option.None<string>();
            const string expected = "bar";

            var result = option.Match(() => expected, _ => "foo");

            result.Should().Be(expected);
        }

        [Test]
        public void Do_Not_Call_OnSome_If_None()
        {
            var option = Option.None<string>();

            option.Match(() => string.Empty, _ => AssertDoNotCalled("OnSome"));
        }

        [Test]
        public void Do_Not_Call_OnNone_If_Some()
        {
            var option = Option.Some("foo");

            option.Match(() => AssertDoNotCalled("OnNone"), x => string.Empty);
        }

        [AssertionMethod]
        private static string AssertDoNotCalled(string branch)
        {
            Assert.Fail(branch + " called");
            return string.Empty;
        }
    }
}
