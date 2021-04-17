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
    internal class None_Should
    {
        [Test]
        public void Throw_If_Some()
        {
            var option = Option<int>.Some(5);

            Action action = () => option.EnsureNone();

            action.Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void Throw_ValueExistsException_If_Some()
        {
            const string expected = "value";
            var option = Option<string>.Some(expected);

            Action action = () => option.EnsureNone();

            action.Should()
                .Throw<ValueExistsException>()
                .WithMessage($"*{expected}*");
        }

        private static TestCaseData CreateCase(Action<Option<int>> extractor)
        {
            return new(extractor);
        }

        private static readonly TestCaseData[] MyExceptionCases =
        {
            CreateCase(option => option.EnsureNone(new MyException())),
            CreateCase(option => option.EnsureNone(() => new MyException())),
            CreateCase(option => option.EnsureNone(_ => new MyException())),
        };

        [TestCaseSource(nameof(MyExceptionCases))]
        public void Throw_MyException_If_Some(Action<Option<int>> extractor)
        {
            var option = Option<int>.Some(5);

            Action action = () => extractor(option);

            action.Should().Throw<MyException>();
        }

        [Test]
        public void Pass_Value_To_Exception_Factory()
        {
            const string expected = "example";
            var option = Option<string>.Some(expected);

            Action action = () => option.EnsureNone(value => new Exception(value));

            action.Should()
                .Throw<Exception>()
                .WithMessage(expected);
        }

        private static readonly IEnumerable<TestCaseData> SomeCases = MyExceptionCases
            .Append(CreateCase(option => option.EnsureNone()));

        [TestCaseSource(nameof(SomeCases))]
        public void Do_Not_Throw_If_None(Action<Option<int>> extractor)
        {
            var option = Option<int>.None();

            Action action = () => extractor(option);

            action.Should().NotThrow();
        }

        private static Exception AssertIsNotCalled()
        {
            Assert.Fail("Exception should not be created on none");
            throw new UnreachableException();
        }

        private static TestCaseData CreateDoNoCallFactoryCase(Action<Option<int>> assertExtracted)
        {
            return new(assertExtracted);
        }

        private static readonly TestCaseData[] CreateDoNoCallFactoryOnCases =
        {
            CreateDoNoCallFactoryCase(option => option.EnsureNone(_ => AssertIsNotCalled())),
            CreateDoNoCallFactoryCase(option => option.EnsureNone(AssertIsNotCalled)),
        };

        [TestCaseSource(nameof(CreateDoNoCallFactoryOnCases))]
        public void Do_No_Create_Exception_If_Failure(Action<Option<int>> assertExtracted)
        {
            var option = Option<int>.None();

            assertExtracted(option);
        }

        private class MyException : Exception
        {
        }
    }
}
