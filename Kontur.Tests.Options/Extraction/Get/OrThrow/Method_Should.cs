using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Extraction.Get.OrThrow
{
    [TestFixture]
    internal class Method_Should
    {
        private static TestCaseData CreateCase(Func<Option<int>, int> extractor)
        {
            return new(extractor);
        }

        private static readonly TestCaseData[] MyExceptionCases =
        {
            CreateCase(option => option.GetOrThrow(new MyException())),
            CreateCase(option => option.GetOrThrow(() => new MyException())),
        };

        [TestCaseSource(nameof(MyExceptionCases))]
        public void Throw_MyException_If_None(Func<Option<int>, int> extractor)
        {
            var option = Option<int>.None();

            Func<int> action = () => extractor(option);

            action.Should().Throw<MyException>();
        }

        private static readonly IEnumerable<TestCaseData> SomeCases = MyExceptionCases
            .Append(CreateCase(option => option.GetOrThrow()));

        [TestCaseSource(nameof(SomeCases))]
        public void Return_Value_If_Some(Func<Option<int>, int> extractor)
        {
            const int expected = 5;
            var option = Option<int>.Some(expected);

            var result = extractor(option);

            result.Should().Be(expected);
        }

        private static Exception AssertIsNotCalled()
        {
            Assert.Fail("Exception should not be created on some");
            throw new UnreachableException();
        }

        [Test]
        public void Do_No_Create_Exception_If_Some()
        {
            var option = Option<int>.Some(5);

            option.GetOrThrow(AssertIsNotCalled);
        }

        private static IEnumerable<Func<Option<TSource>, TResult>> GetMethods<TSource, TResult>()
            where TSource : class, TResult
        {
            yield return option => option.GetOrThrow<TResult>();
            yield return option => option.GetOrThrow<TResult>(new MyException());
            yield return option => option.GetOrThrow<TResult>(() => new MyException());
        }

        private static IEnumerable<TestCaseData> GetUpcastCases()
        {
            return GetMethods<Child, Base>()
                .Select(method => new TestCaseData(method));
        }

        [TestCaseSource(nameof(GetUpcastCases))]
        public void Upcast_On_Some(Func<Option<Child>, Base> callGetOrThrow)
        {
            Child expected = new();
            var option = Option<Child>.Some(expected);

            var value = callGetOrThrow(option);

            value.Should().Be(expected);
        }

        [TestCaseSource(nameof(GetUpcastCases))]
        public void Upcast_On_None(Func<Option<Child>, Base> callGetOrThrow)
        {
            var option = Option<Child>.None();

            Func<Base> action = () => callGetOrThrow(option);

            action.Should().Throw<Exception>();
        }

        private class MyException : Exception
        {
        }
    }
}
