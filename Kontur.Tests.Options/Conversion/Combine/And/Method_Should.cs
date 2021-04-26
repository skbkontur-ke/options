using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Conversion.Combine.And
{
    [TestFixture]
    internal class Method_Should
    {
        private static IEnumerable<(Option<string> Option1, Option<int> Option2, Option<int> Result)> CreateCases()
        {
            yield return (Option<string>.None(), Option<int>.None(), Option<int>.None());
            yield return (Option<string>.Some("unused"), Option<int>.None(), Option<int>.None());
            yield return (Option<string>.None(), Option<int>.Some(1), Option<int>.None());

            var some = Option<int>.Some(1);
            yield return (Option<string>.Some("unused"), some, some);
        }

        private static readonly Func<Option<string>, Option<int>, Option<int>>[] Methods =
        {
            (option1, option2) => option1.And(option2),
            (option1, option2) => option1.And(() => option2),
            (option1, option2) => option1.And(_ => option2),
        };

        private static readonly IEnumerable<TestCaseData> Cases =
            from testCase in CreateCases()
            from method in Methods
            select new TestCaseData(testCase.Option1, testCase.Option2, method).Returns(testCase.Result);

        [TestCaseSource(nameof(Cases))]
        public Option<int> Process(
            Option<string> option1,
            Option<int> option2,
            Func<Option<string>, Option<int>, Option<int>> callAnd)
        {
            return callAnd(option1, option2);
        }

        [Test]
        public void Pass_Value_If_Some()
        {
            var option = Option<string>.Some("example");

            var result = option.And(Option<string>.Some);

            result.Should().Be(option);
        }

        private static Option<int> AssertIsNotCalled()
        {
            Assert.Fail("Value factory should not be called on None");
            throw new UnreachableException();
        }

        private static readonly Func<Option<int>, Option<int>>[] AssertIsNotCalledMethods =
        {
            option => option.And(AssertIsNotCalled),
            option => option.And(_ => AssertIsNotCalled()),
        };

        private static readonly IEnumerable<TestCaseData> AssertIsNotCalledCases =
            AssertIsNotCalledMethods.Select(method => new TestCaseData(method));

        [TestCaseSource(nameof(AssertIsNotCalledCases))]
        public void Do_Not_Call_Delegate_If_None(Func<Option<int>, Option<int>> assertIsNotCalled)
        {
            var option = Option<int>.None();

            assertIsNotCalled(option);
        }
    }
}
