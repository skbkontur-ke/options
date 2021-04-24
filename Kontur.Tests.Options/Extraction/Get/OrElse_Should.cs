using System;
using System.Collections.Generic;
using System.Linq;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Extraction.Get
{
    [TestFixture]
    internal class OrElse_Should
    {
        private static int AssertIsNotCalled()
        {
            Assert.Fail("Backup value factory should not be called on Some");
            throw new UnreachableException();
        }

        [Test]
        public void Do_Not_Call_Delegate_If_Some()
        {
            var option = Option<int>.Some(0);

            option.GetOrElse(AssertIsNotCalled);
        }

        private static IEnumerable<Func<Option<TSource>, TResult>> GetMethods<TSource, TResult>(TResult defaultValue)
            where TSource : class, TResult
        {
            yield return option => option.GetOrElse(defaultValue);
            yield return option => option.GetOrElse(() => defaultValue);
        }

        private static IEnumerable<TestCaseData> GetStringCases()
        {
            const string defaultValue = "default on none";
            const string someValue = "bar";

            var testCases = new (Option<string> Option, string Result)[]
            {
                (Option<string>.None(), defaultValue),
                (Option<string>.Some(someValue), someValue),
            };

            return
                from testCase in testCases
                from method in GetMethods<string, string>(defaultValue)
                select new TestCaseData(testCase.Option, method).Returns(testCase.Result);
        }

        [TestCaseSource(nameof(GetStringCases))]
        public string Return_Result(Option<string> option, Func<Option<string>, string> callGetOrElse)
        {
            return callGetOrElse(option);
        }

        private static IEnumerable<TestCaseData> GetUpcastCases()
        {
            Base defaultValue = new();

            return
                from testCase in UpcastExamples.Get(defaultValue, value => value)
                from method in GetMethods<Child, Base>(defaultValue)
                select new TestCaseData(testCase.Option, method).Returns(testCase.Result);
        }

        [TestCaseSource(nameof(GetUpcastCases))]
        public Base Upcast(Option<Child> option, Func<Option<Child>, Base> callGetOrElse)
        {
            return callGetOrElse(option);
        }

        private static IEnumerable<Func<Option<Base>, Base>> GetUpcastDefaultValueMethods(Child defaultValue)
        {
            yield return option => option.GetOrElse(defaultValue);
            yield return option => option.GetOrElse(() => defaultValue);
        }

        private static IEnumerable<TestCaseData> GetUpcastDefaultValueCases()
        {
            Child defaultValue = new();
            Base someValue = new();

            (Option<Base> Option, Base Result)[] cases =
            {
                (Option<Base>.Some(someValue), someValue),
                (Option<Base>.None(), defaultValue),
            };

            return
                from testCase in cases
                from method in GetUpcastDefaultValueMethods(defaultValue)
                select new TestCaseData(testCase.Option, method).Returns(testCase.Result);
        }

        [TestCaseSource(nameof(GetUpcastDefaultValueCases))]
        public Base Upcast_Default_Value(Option<Base> option, Func<Option<Base>, Base> callGetOrElse)
        {
            return callGetOrElse(option);
        }
    }
}
