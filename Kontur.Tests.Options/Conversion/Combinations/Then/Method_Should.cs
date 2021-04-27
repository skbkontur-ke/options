using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using FluentAssertions;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Conversion.Combinations.Then
{
    [TestFixture]
    internal class Method_Should
    {
        private static IEnumerable<(Option<int> Option1, Option<string> Option2, Option<string> Result)> CreateCases()
        {
            yield return (Option<int>.None(), Option<string>.None(), Option<string>.None());
            yield return (Option<int>.Some(1), Option<string>.None(), Option<string>.None());
            yield return (Option<int>.None(), Option<string>.Some("unused"), Option<string>.None());

            var some = Option<string>.Some("value");
            yield return (Option<int>.Some(1), some, some);
        }

        private static readonly Func<Option<int>, Func<int, Option<string>>, Option<string>> PassMethod = (option, factory) => option.Then(factory);

        private static readonly Func<Option<int>, Func<Option<string>>, Option<string>>[] FactoryMethods =
        {
            (option, factory) => option.Then(factory),
            (option, factory) => PassMethod(option, _ => factory()),
        };

        private static IEnumerable<Func<Option<int>, Option<string>, Option<string>>> AllMethods()
        {
            yield return (option1, option2) => option1.Then(option2);
            foreach (var method in FactoryMethods)
            {
                yield return (option1, option2) => method(option1, () => option2);
            }
        }

        private static readonly IEnumerable<TestCaseData> Cases =
            from testCase in CreateCases()
            from method in AllMethods()
            select new TestCaseData(testCase.Option1, testCase.Option2, method).Returns(testCase.Result);

        [TestCaseSource(nameof(Cases))]
        public Option<string> Process(
            Option<int> option1,
            Option<string> option2,
            Func<Option<int>, Option<string>, Option<string>> then)
        {
            return then(option1, option2);
        }

        [Test]
        public void Pass_Value_If_Some()
        {
            var option = Option<int>.Some(123);

            var result = PassMethod(option, i => Option<string>.Some(i.ToString(CultureInfo.InvariantCulture)));

            result.Should().Be(Option<string>.Some("123"));
        }

        private static Option<string> AssertIsNotCalled()
        {
            Assert.Fail("Factory should not be called on None");
            throw new UnreachableException();
        }

        private static readonly Func<Option<int>, Option<string>>[] AssertIsNotCalledMethods =
        {
            option => option.Then(AssertIsNotCalled),
            option => option.Then(_ => AssertIsNotCalled()),
        };

        private static readonly IEnumerable<TestCaseData> AssertIsNotCalledCases =
            AssertIsNotCalledMethods.Select(method => new TestCaseData(method));

        [TestCaseSource(nameof(AssertIsNotCalledCases))]
        public void Do_Not_Call_Delegate_If_None(Func<Option<int>, Option<string>> assertIsNotCalled)
        {
            var option = Option<int>.None();

            assertIsNotCalled(option);
        }
    }
}
