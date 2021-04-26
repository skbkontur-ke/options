using System.Collections.Generic;
using System.Linq;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Conversion.Combinations.Or
{
    [TestFixture]
    internal class Upcast_Should
    {
        public delegate Option<Base> Or<TValue1, TValue2>(Option<TValue1> option1, Option<TValue2> option2)
            where TValue1 : Base
            where TValue2 : Base;

        private static IEnumerable<(Option<TValue1> Option1, Option<TValue2> Option2, Option<Base> Result)> CreateCases<TValue1, TValue2>()
            where TValue1 : Base, new()
            where TValue2 : Base, new()
        {
            yield return (Option<TValue1>.None(), Option<TValue2>.None(), Option<Base>.None());

            TValue2 example2 = new();
            yield return (Option<TValue1>.None(), Option<TValue2>.Some(example2), Option<Base>.Some(example2));

            TValue1 example1 = new();
            yield return (Option<TValue1>.Some(example1), Option<TValue2>.None(), Option<Base>.Some(example1));
            yield return (Option<TValue1>.Some(example1), Option<TValue2>.Some(new()), Option<Base>.Some(example1));
        }

        private static readonly Or<Child, Base>[] FirstMethods =
        {
            (option1, option2) => option1.Or(option2),
            (option1, option2) => option1.Or(() => option2),
        };

        private static readonly IEnumerable<TestCaseData> FirstCases =
            from testCase in CreateCases<Child, Base>()
            from method in FirstMethods
            select new TestCaseData(testCase.Option1, testCase.Option2, method).Returns(testCase.Result);

        [TestCaseSource(nameof(FirstCases))]
        public Option<Base> First(Option<Child> option1, Option<Base> option2, Or<Child, Base> or)
        {
            return or(option1, option2);
        }

        private static readonly Or<Base, Child>[] SecondMethods =
        {
            (option1, option2) => option1.Or(option2),
            (option1, option2) => option1.Or(() => option2),
        };

        private static readonly IEnumerable<TestCaseData> SecondCases =
            from testCase in CreateCases<Base, Child>()
            from method in SecondMethods
            select new TestCaseData(testCase.Option1, testCase.Option2, method).Returns(testCase.Result);

        [TestCaseSource(nameof(SecondCases))]
        public Option<Base> Second(Option<Base> option1, Option<Child> option2, Or<Base, Child> or)
        {
            return or(option1, option2);
        }

        private static readonly Or<Child, Child>[] ExplicitMethods =
        {
            (option1, option2) => option1.Or<Base>(option2),
            (option1, option2) => option1.Or<Base>(() => option2),
        };

        private static readonly IEnumerable<TestCaseData> ExplicitCases =
            from testCase in CreateCases<Child, Child>()
            from method in ExplicitMethods
            select new TestCaseData(testCase.Option1, testCase.Option2, method).Returns(testCase.Result);

        [TestCaseSource(nameof(ExplicitCases))]
        public Option<Base> Explicit(Option<Child> option1, Option<Child> option2, Or<Child, Child> or)
        {
            return or(option1, option2);
        }
    }
}
