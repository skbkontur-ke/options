using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Kontur.Options;
using Kontur.Options.Unsafe;
using NUnit.Framework;

namespace Kontur.Tests.Options.Extraction
{
    [TestFixture]
    internal class GetOrThrow_Should
    {
        [Test]
        public void Throw_InvalidOperationException_On_None()
        {
            var option = Option.None<int>();

            Func<int> action = () => option.GetOrThrow();

            action.Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void Throw_ValueMissingException_On_None()
        {
            var option = Option.None<int>();

            Func<int> action = () => option.GetOrThrow();

            action.Should().Throw<ValueMissingException>();
        }

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
        public void Throw_MyException_On_None(Func<Option<int>, int> extractor)
        {
            var option = Option.None<int>();

            Func<int> action = () => extractor(option);

            action.Should().Throw<MyException>();
        }

        private static readonly IEnumerable<TestCaseData> SomeCases = MyExceptionCases
            .Append(CreateCase(option => option.GetOrThrow()));

        [TestCaseSource(nameof(SomeCases))]
        public void Return_Value_On_Some(Func<Option<int>, int> extractor)
        {
            const int expected = 5;
            var option = Option.Some(expected);

            var result = extractor(option);

            result.Should().Be(expected);
        }

        [Test]
        public void Do_No_Create_Exception_On_Some()
        {
            var option = Option.Some(5);

            option.GetOrThrow(AssertDoNotCalled);
        }

        private static Exception AssertDoNotCalled()
        {
            const string message = "Exception should not be created on some";
            Assert.Fail(message);
            return new Exception(message);
        }

        private class MyException : Exception
        {
        }
    }
}
