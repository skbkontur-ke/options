using System;
using System.Collections.Generic;
using System.Linq;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Conversion.Combinations.OrElse
{
    [TestFixture]
    internal class Method_Should
    {
        private static IEnumerable<(Option<string> Option1, Option<string> Option2, Option<string> Result)> CreateCases()
        {
            var none = Option<string>.None();
            var some = Option<string>.Some("example");

            yield return (none, none, none);
            yield return (some, none, some);
            yield return (none, some, some);
            yield return (some, Option<string>.Some("unused"), some);
        }

        private static readonly Func<Option<string>, Func<Option<string>>, Option<string>> FactoryMethod = (option, factory) => option.OrElse(factory);

        private static readonly Func<Option<string>, Option<string>, Option<string>>[] AllMethods =
        {
            (option1, option2) => option1.OrElse(option2),
            (option1, option2) => FactoryMethod(option1, () => option2),
        };

        private static readonly IEnumerable<TestCaseData> Cases =
            from testCase in CreateCases()
            from method in AllMethods
            select new TestCaseData(testCase.Option1, testCase.Option2, method).Returns(testCase.Result);

        [TestCaseSource(nameof(Cases))]
        public Option<string> Process(
            Option<string> option1,
            Option<string> option2,
            Func<Option<string>, Option<string>, Option<string>> orElse)
        {
            return orElse(option1, option2);
        }

        private static Option<string> AssertIsNotCalled()
        {
            Assert.Fail("Factory should not be called on Some");
            throw new UnreachableException();
        }

        [Test]
        public void Do_Not_Call_Delegate_If_Some()
        {
            var option = Option<string>.Some("value");

            FactoryMethod(option, AssertIsNotCalled);
        }
    }
}
