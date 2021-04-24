using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using FluentAssertions;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Conversion
{
    [TestFixture]
    internal class Map_Should
    {
        private const int ExpectedResult = 42;

        private static readonly Func<Option<string>, Option<int>>[] Methods =
        {
            result => result.Map(_ => ExpectedResult),
            result => result.Map(() => ExpectedResult),
            result => result.Map(ExpectedResult),
        };

        private static readonly IEnumerable<TestCaseData> MapCases =
            from testCase in new[]
            {
                (Source: Option<string>.None(), Result: Option<int>.None()),
                (Source: Option<string>.Some("unused"), Result: Option<int>.Some(ExpectedResult)),
            }
            from method in Methods
            select new TestCaseData(testCase.Source, method).Returns(testCase.Result);

        [TestCaseSource(nameof(MapCases))]
        public Option<int> Convert(Option<string> source, Func<Option<string>, Option<int>> converter)
        {
            return converter(source);
        }

        [Test]
        public void Use_Value()
        {
            var option = Option<int>.Some(777);

            var result = option.Map(i => i.ToString(CultureInfo.InvariantCulture));

            result.Should().BeEquivalentTo(Option<string>.Some("777"));
        }

        private static int AssertIsNotCalled()
        {
            Assert.Fail("Value factory should not be called on None");
            throw new UnreachableException();
        }

        private static TestCaseData CreateDoNoCallFactoryCase(Func<Option<string>, Option<int>> assertMapped)
        {
            return new(assertMapped);
        }

        private static readonly TestCaseData[] CreateDoNoCallSomeFactoryIfNoneCases =
        {
            CreateDoNoCallFactoryCase(option => option.Map(_ => AssertIsNotCalled())),
            CreateDoNoCallFactoryCase(option => option.Map(AssertIsNotCalled)),
        };

        [TestCaseSource(nameof(CreateDoNoCallSomeFactoryIfNoneCases))]
        public void Do_Not_Call_Delegate_If_None(Func<Option<string>, Option<int>> assertMapped)
        {
            var option = Option<string>.None();

            assertMapped(option);
        }
    }
}