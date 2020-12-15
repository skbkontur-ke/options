using FluentAssertions;
using JetBrains.Annotations;
using Kontur.Extern.Options;
using NUnit.Framework;

namespace Kontur.Extern.Tests.Options.Extraction
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

            var result = option.Match(val => val + expectedSuffix, () => "bar");

            result.Should().Be(expectedPrefix + expectedSuffix);
        }

        [Test]
        public void Return_OnNone()
        {
            var option = Option.None<string>();
            const string expected = "bar";

            var result = option.Match(_ => "foo", () => expected);

            result.Should().Be(expected);
        }

        [Test]
        public void Do_Not_Call_OnSome_If_None()
        {
            var option = Option.None<string>();

            option.Match(
                _ => AssertDoNotCalled("OnSome"),
                () => string.Empty);
        }

        [Test]
        public void Do_Not_Call_OnNone_If_Some()
        {
            var option = Option.Some("foo");

            option.Match(
                x => string.Empty,
                () => AssertDoNotCalled("OnNone"));
        }

        [AssertionMethod]
        private static string AssertDoNotCalled(string branch)
        {
            Assert.Fail(branch + " called");
            return string.Empty;
        }
    }
}
