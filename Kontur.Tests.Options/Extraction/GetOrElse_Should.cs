using System;
using System.Collections.Generic;
using FluentAssertions;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Extraction
{
    [TestFixture]
    internal class GetOrElse_Should
    {
        private static int AssertIsNotCalled()
        {
            Assert.Fail("Backup value factory should not be called on Some");
            throw new UnreachableException();
        }

        [Test]
        public void Do_Not_Call_Delegate_On_Some()
        {
            var options = Option<int>.Some(0);

            options.GetOrElse(AssertIsNotCalled);
        }

        private static TestCaseData Create<TSource, TResult>(Func<Option<TSource>, TResult, TResult> extractor)
            where TSource : TResult
        {
            return new(extractor);
        }

        private static IEnumerable<TestCaseData> GetCases<TSource, TResult>()
            where TSource : class, TResult
        {
            yield return Create<TSource, TResult>((option, defaultValue) => option.GetOrElse(defaultValue));
            yield return Create<TSource, TResult>((option, defaultValue) => option.GetOrElse(() => defaultValue));
        }

        private static readonly IEnumerable<TestCaseData> StringCases = GetCases<string, string>();

        [TestCaseSource(nameof(StringCases))]
        public void Return_Default_Value_OnNone(Func<Option<string>, string, string> extractor)
        {
            const string expectedResult = "default on none";
            var option = Option<string>.None();

            var result = extractor(option, expectedResult);

            result.Should().Be(expectedResult);
        }

        [TestCaseSource(nameof(StringCases))]
        public void Return_Value_OnSome(Func<Option<string>, string, string> extractor)
        {
            const string expectedResult = "hello";
            var option = Option<string>.Some(expectedResult);

            var result = extractor(option, "ignored");

            result.Should().Be(expectedResult);
        }

        private static readonly IEnumerable<TestCaseData> UpcastCases = GetCases<B, A>();

        [TestCaseSource(nameof(UpcastCases))]
        public void Upcast_OnNone(Func<Option<B>, A, A> extractor)
        {
            var expectedResult = new A();
            var option = Option<B>.None();

            var result = extractor(option, expectedResult);

            result.Should().Be(expectedResult);
        }

        [TestCaseSource(nameof(UpcastCases))]
        public void Upcast_OnSome(Func<Option<B>, A, A> extractor)
        {
            var expectedResult = new B();
            var option = Option<B>.Some(expectedResult);

            var result = extractor(option, new A());

            result.Should().Be(expectedResult);
        }

        public class A
        {
        }

        public class B : A
        {
        }
    }
}
