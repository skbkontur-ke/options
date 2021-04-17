using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Kontur.Options;
using Kontur.Options.Unsafe;
using NUnit.Framework;

namespace Kontur.Tests.Options.Extraction.Ensure
{
    [TestFixture]
    internal class HasValue_Should
    {
        [Test]
        public void Throw_If_None()
        {
            var option = Option<int>.None();

            Action action = () => option.EnsureHasValue();

            action.Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void Throw_ValueMissingException_If_None()
        {
            var option = Option<int>.None();

            Action action = () => option.EnsureHasValue();

            action.Should().Throw<ValueMissingException>();
        }

        private static TestCaseData CreateCase(Action<Option<int>> extractor)
        {
            return new(extractor);
        }

        private static readonly TestCaseData[] MyExceptionCases =
        {
            CreateCase(option => option.EnsureHasValue(new MyException())),
            CreateCase(option => option.EnsureHasValue(() => new MyException())),
        };

        [TestCaseSource(nameof(MyExceptionCases))]
        public void Throw_MyException_If_None(Action<Option<int>> extractor)
        {
            var option = Option<int>.None();

            Action action = () => extractor(option);

            action.Should().Throw<MyException>();
        }

        private static readonly IEnumerable<TestCaseData> SomeCases = MyExceptionCases
            .Append(CreateCase(option => option.EnsureHasValue()));

        [TestCaseSource(nameof(SomeCases))]
        public void Do_Not_Throw_If_Some(Action<Option<int>> extractor)
        {
            var option = Option<int>.Some(5);

            Action action = () => extractor(option);

            action.Should().NotThrow();
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

            option.EnsureHasValue(AssertIsNotCalled);
        }


        private class MyException : Exception
        {
        }
    }
}
