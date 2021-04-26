using System;
using System.Collections.Generic;
using System.Linq;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Conversion.Combinations.And
{
    [TestFixture]
    internal class Upcast_Should
    {
        private static IEnumerable<(Option<string> Option1, Option<Child> Option2, Option<Base> Result)> CreateCases()
        {
            yield return (Option<string>.None(), Option<Child>.None(), Option<Base>.None());
            yield return (Option<string>.Some("unused"), Option<Child>.None(), Option<Base>.None());
            yield return (Option<string>.None(), Option<Child>.Some(new()), Option<Base>.None());

            Child value = new();
            yield return (Option<string>.Some("unused"), Option<Child>.Some(value), Option<Base>.Some(value));
        }

        private static readonly Func<Option<string>, Option<Child>, Option<Base>>[] Methods =
        {
            (option1, option2) => option1.Then<Base>(option2),
            (option1, option2) => option1.Then<Base>(() => option2),
            (option1, option2) => option1.Then<Base>(_ => option2),
        };

        private static readonly IEnumerable<TestCaseData> Cases =
            from testCase in CreateCases()
            from method in Methods
            select new TestCaseData(testCase.Option1, testCase.Option2, method).Returns(testCase.Result);

        [TestCaseSource(nameof(Cases))]
        public Option<Base> Process(
            Option<string> option1,
            Option<Child> option2,
            Func<Option<string>, Option<Child>, Option<Base>> callAnd)
        {
            return callAnd(option1, option2);
        }
    }
}