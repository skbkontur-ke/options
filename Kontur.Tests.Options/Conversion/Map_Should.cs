using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using FluentAssertions;
using JetBrains.Annotations;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Conversion
{
    [TestFixture]
    internal class Map_Should
    {
        private const int ExpectedResult = 42;

        private static TestCaseData CreateMapCase(Func<Option<string>, Option<int>> converter)
        {
            return new(converter);
        }

        private static IEnumerable<TestCaseData> MapCases
        {
            get
            {
                yield return CreateMapCase(option => option.Map(_ => ExpectedResult));
                yield return CreateMapCase(option => option.Map(() => ExpectedResult));
                yield return CreateMapCase(option => option.Map(ExpectedResult));
            }
        }

        [TestCaseSource(nameof(MapCases))]
        public void Do_Not_Convert_None(Func<Option<string>, Option<int>> converter)
        {
            var option = Option<string>.None();

            var result = converter(option);

            result.Should().BeEquivalentTo(Option.None<int>());
        }

        private static readonly IEnumerable<TestCaseData> ConvertSomeCases = MapCases
            .Select(testCase => testCase.Returns(Option.Some(ExpectedResult)));

        [TestCaseSource(nameof(ConvertSomeCases))]
        public Option<int> Convert_Some(Func<Option<string>, Option<int>> converter)
        {
            var option = Option.Some("unused");

            return converter(option);
        }

        [Test]
        public void Use_Value()
        {
            var option = Option.Some(777);

            var result = option.Map(i => i.ToString(CultureInfo.InvariantCulture));

            result.Should().BeEquivalentTo(Option.Some("777"));
        }

        [Test]
        public void Do_Not_Call_Delegate_On_None()
        {
            var option = Option<int>.None();

            option.Map(ThrowError);
        }

        [Test]
        public void Do_Not_Call_Delegate_With_Param_On_None()
        {
            var option = Option<int>.None();

            option.Map(_ => ThrowError());
        }

        [AssertionMethod]
        private static int ThrowError()
        {
            Assert.Fail("Value factory should not be called on None");
            throw new UnreachableException();
        }
    }
}