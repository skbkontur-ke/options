﻿using System;
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
        private static IEnumerable<Func<Option<int>, string>> CreateAllMatchMethods(string onNoneValue, string onSomeValue)
        {
            yield return option => option.Match(() => onNoneValue, _ => onSomeValue);
            yield return option => option.Match(() => onNoneValue, () => onSomeValue);
            yield return option => option.Match(() => onNoneValue, onSomeValue);
            yield return option => option.Match(onNoneValue, _ => onSomeValue);
            yield return option => option.Match(onNoneValue, () => onSomeValue);
            yield return option => option.Match(onNoneValue, onSomeValue);
        }

        private static IEnumerable<TestCaseData> ReturnOnSomeCases
        {
            get
            {
                const string expected = "some-value";

                return CreateAllMatchMethods("unreachable", expected)
                    .Select(method => new TestCaseData(method).Returns(expected));
            }
        }

        [TestCaseSource(nameof(ReturnOnSomeCases))]
        public string Return_OnSome(Func<Option<int>, string> callMatch)
        {
            var option = Option.Some(0);

            return callMatch(option);
        }

        private static IEnumerable<TestCaseData> ReturnOnNoneCases
        {
            get
            {
                const string expected = "none-value";

                return CreateAllMatchMethods(expected, "unreachable")
                    .Select(method => new TestCaseData(method).Returns(expected));
            }
        }

        [TestCaseSource(nameof(ReturnOnNoneCases))]
        public string Return_OnNone(Func<Option<int>, string> callMatch)
        {
            var option = Option.None<int>();

            return callMatch(option);
        }

        private static TestCaseData CreateUseValueCase(Func<Option<int>, string> extractor)
        {
            return new(extractor);
        }

        private static IEnumerable<TestCaseData> UseValueCases
        {
            get
            {
                yield return CreateUseValueCase(option => option.Match(
                    "unused",
                    i => i.ToString(CultureInfo.InvariantCulture)));

                yield return CreateUseValueCase(option => option.Match(
                    () => "unused",
                    i => i.ToString(CultureInfo.InvariantCulture)));
            }
        }

        [TestCaseSource(nameof(UseValueCases))]
        public void Use_Value(Func<Option<int>, string> extractor)
        {
            var option = Option.Some(777);

            var result = extractor(option);

            result.Should().Be("777");
        }

        private static TestCaseData CreateDoNoCallFactoryCase(Func<Option<string>, string> assertExtracted)
        {
            return new(assertExtracted);
        }

        private static string AssertSomeIsNotCalled()
        {
            return AssertDoNotCalled("OnSome");
        }

        private static readonly TestCaseData[] CreateDoNoCallSomeFactoryOnNoneCases =
        {
            CreateDoNoCallFactoryCase(option => option.Match(() => "unreachable", _ => AssertSomeIsNotCalled())),
            CreateDoNoCallFactoryCase(option => option.Match(() => "unreachable", AssertSomeIsNotCalled)),
            CreateDoNoCallFactoryCase(option => option.Match("unreachable", _ => AssertSomeIsNotCalled())),
            CreateDoNoCallFactoryCase(option => option.Match("unreachable", AssertSomeIsNotCalled)),
        };

        [TestCaseSource(nameof(CreateDoNoCallSomeFactoryOnNoneCases))]
        public void Do_Not_Call_OnSome_Factory_If_None(Func<Option<string>, string> assertExtracted)
        {
            var option = Option.None<string>();

            assertExtracted(option);
        }

        private static string AssertNoneIsNotCalled()
        {
            return AssertDoNotCalled("OnNone");
        }

        private static readonly TestCaseData[] CreateDoNoCallNoneFactoryOnSomeCases =
        {
            CreateDoNoCallFactoryCase(option => option.Match(AssertNoneIsNotCalled, _ => "unreachable")),
            CreateDoNoCallFactoryCase(option => option.Match(AssertNoneIsNotCalled,  () => "unreachable")),
            CreateDoNoCallFactoryCase(option => option.Match(AssertNoneIsNotCalled,  "unreachable")),
        };

        [TestCaseSource(nameof(CreateDoNoCallNoneFactoryOnSomeCases))]
        public void Do_Not_Call_OnNone_If_Some(Func<Option<string>, string> assertExtracted)
        {
            var option = Option.Some("foo");

            assertExtracted(option);
        }

        [AssertionMethod]
        private static string AssertDoNotCalled(string branch)
        {
            Assert.Fail(branch + " is called");
            throw new UnreachableException();
        }
    }
}
