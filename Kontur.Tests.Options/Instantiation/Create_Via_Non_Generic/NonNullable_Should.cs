using System;
using FluentAssertions;
using Kontur.Options;
using Kontur.Options.Unsafe;
using NUnit.Framework;

namespace Kontur.Tests.Options.Instantiation.Create_Via_Non_Generic
{
    [TestFixture(10)]
    [TestFixture("bar")]
    internal class NonNullable_Should<T>
    {
        private readonly T value;

        public NonNullable_Should(T value)
        {
        this.value = value;
        }

        private static TestCaseData Create(Func<T, Option<T>> optionFactory, bool hasValue)
        {
            return new TestCaseData(optionFactory).Returns(hasValue);
        }

        private static readonly TestCaseData[] Cases =
        {
            Create(_ => Option.None<T>(), false),
            Create(Option.Some, true),
        };

        [TestCaseSource(nameof(Cases))]
        public bool HasValue(Func<T, Option<T>> optionFactory)
        {
            var option = optionFactory(value);

            return option.HasSome;
        }

        [Test]
        public void Store_Value()
        {
            var option = Option.Some(value);

            var result = option.GetOrThrow();

            result.Should().Be(value);
        }
    }
}
