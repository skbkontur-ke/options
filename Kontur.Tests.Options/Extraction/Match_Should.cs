using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using FluentAssertions;
using JetBrains.Annotations;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Extraction
{
    [TestFixture]
    internal class Match_Should
    {
        private static string ToString(int value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }

        private static IEnumerable<Func<Option<int>, string>> CreateAllMatchMethods(string onNoneValue, string onSomeValue)
        {
            yield return option => option.Match(() => onNoneValue, ToString);
            yield return option => option.Match(() => onNoneValue, () => onSomeValue);
            yield return option => option.Match(() => onNoneValue, onSomeValue);
            yield return option => option.Match(onNoneValue, ToString);
            yield return option => option.Match(onNoneValue, () => onSomeValue);
            yield return option => option.Match(onNoneValue, onSomeValue);
        }

        private static IEnumerable<TestCaseData> ReturnOnSomeCases
        {
            get
            {
                const int expected = 42;
                var expectedString = ToString(expected);

                return CreateAllMatchMethods("unreachable", expectedString)
                    .Select(method => new TestCaseData(expected, method));
            }
        }

        [TestCaseSource(nameof(ReturnOnSomeCases))]
        public void Return_OnSome(int expected, Func<Option<int>, string> callMatch)
        {
            var option = Option.Some(expected);

            var result = callMatch(option);

            result.Should().Be(ToString(expected));
        }

        private static IEnumerable<TestCaseData> ReturnOnNoneCases
        {
            get
            {
                const string result = "on none branch";
                return CreateAllMatchMethods(result, "unreachable")
                    .Select(method => new TestCaseData(method).Returns(result));
            }
        }

        [TestCaseSource(nameof(ReturnOnNoneCases))]
        public string Return_OnNone(Func<Option<int>, string> callMatch)
        {
            var option = Option.None<int>();

            return callMatch(option);
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

            option.Match(() => AssertDoNotCalled("OnNone"), _ => string.Empty);
        }

        [AssertionMethod]
        private static string AssertDoNotCalled(string branch)
        {
            Assert.Fail(branch + " called");
            return string.Empty;
        }
    }
}
