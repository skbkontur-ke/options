using System;
using FluentAssertions;
using Kontur.Options;
using Kontur.Options.Unsafe;
using NUnit.Framework;

namespace Kontur.Tests.Options.Instantiation
{
    [TestFixture(10)]
    [TestFixture("bar")]
    internal class Create_Via_Generic_Should<T>
    {
        private readonly T value;

        public Create_Via_Generic_Should(T value)
        {
            this.value = value;
        }

        private static TestCaseData CreateCase(Func<T, Option<T>> optionFactory, bool hasSome)
        {
            return new TestCaseData(optionFactory).Returns(hasSome);
        }

        private static readonly TestCaseData[] CreateCases =
        {
            CreateCase(_ => Option<T>.None(), false),
            CreateCase(Option<T>.Some, true),
        };

        [TestCaseSource(nameof(CreateCases))]
        public bool HasValue(Func<T, Option<T>> optionFactory)
        {
            var option = optionFactory(value);

            return option.HasSome;
        }

        [Test]
        public void Store_Value()
        {
            var option = Option<T>.Some(value);

            var result = option.GetOrThrow();

            result.Should().Be(value);
        }
    }
}
