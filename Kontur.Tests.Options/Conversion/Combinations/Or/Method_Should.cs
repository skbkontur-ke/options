using System;
using System.Collections.Generic;
using System.Linq;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Conversion.Combinations.Or
{
    [TestFixture]
    internal class Method_Should
    {
        private static IEnumerable<(Option<int> Option1, Option<int> Option2, Option<int> Result)> CreateCases()
        {
            var none = Option<int>.None();
            var some = Option<int>.Some(1);

            yield return (none, none, none);
            yield return (some, none, some);
            yield return (none, some, some);
            yield return (some, Option<int>.Some(2), some);
        }

        private static readonly Func<Option<int>, Option<int>, Option<int>>[] Methods =
        {
            (option1, option2) => option1.Or(option2),
            (option1, option2) => option1.OrElse(() => option2),
        };

        private static readonly IEnumerable<TestCaseData> Cases =
            from testCase in CreateCases()
            from method in Methods
            select new TestCaseData(testCase.Option1, testCase.Option2, method).Returns(testCase.Result);

        [TestCaseSource(nameof(Cases))]
        public Option<int> Process(
            Option<int> option1,
            Option<int> option2,
            Func<Option<int>, Option<int>, Option<int>> callOr)
        {
            return callOr(option1, option2);
        }

        private static Option<int> AssertIsNotCalled()
        {
            Assert.Fail("Backup value factory should not be called on Some");
            throw new UnreachableException();
        }

        private static readonly Func<Option<int>, Option<int>>[] AssertIsNotCalledMethods =
        {
            option => option.OrElse(AssertIsNotCalled),
        };

        private static readonly IEnumerable<TestCaseData> AssertIsNotCalledCases =
            AssertIsNotCalledMethods.Select(method => new TestCaseData(method));

        [TestCaseSource(nameof(AssertIsNotCalledCases))]
        public void Do_Not_Call_Delegate_If_Some(Func<Option<int>, Option<int>> assertIsNotCalled)
        {
            var option = Option<int>.Some(0);

            assertIsNotCalled(option);
        }
    }
}
