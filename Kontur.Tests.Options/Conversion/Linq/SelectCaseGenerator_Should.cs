using System.Collections.Generic;
using FluentAssertions;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Conversion.Linq
{
    [TestFixture]
    internal class SelectCaseGenerator_Should
    {
        private static TestCaseData Create(int argsCount, params SelectCase[] expectedResult)
        {
            return new TestCaseData(argsCount, expectedResult);
        }

        private static readonly TestCaseData[] Cases =
        {
            Create(
                1,
                new SelectCase(new[] { Option<int>.None() }, Option.None()),
                new SelectCase(new[] { Option.Some(10) }, 10)),
            Create(
                2,
                new SelectCase(new[] { Option<int>.None(), Option<int>.None() }, Option.None()),
                new SelectCase(new[] { Option.Some(10), Option<int>.None() }, Option.None()),
                new SelectCase(new[] { Option<int>.None(), Option.Some(11) }, Option.None()),
                new SelectCase(new[] { Option.Some(10), Option.Some(11) }, 21)),
            Create(
                3,
                new SelectCase(new[] { Option<int>.None(), Option<int>.None(), Option<int>.None() }, Option.None()),
                new SelectCase(new[] { Option.Some(10), Option<int>.None(), Option<int>.None() }, Option.None()),
                new SelectCase(new[] { Option<int>.None(), Option.Some(11), Option<int>.None() }, Option.None()),
                new SelectCase(new[] { Option.Some(10), Option.Some(11), Option<int>.None() }, Option.None()),
                new SelectCase(new[] { Option<int>.None(), Option<int>.None(), Option.Some(12) }, Option.None()),
                new SelectCase(new[] { Option.Some(10), Option<int>.None(), Option.Some(12) }, Option.None()),
                new SelectCase(new[] { Option<int>.None(), Option.Some(11), Option.Some(12) }, Option.None()),
                new SelectCase(new[] { Option.Some(10), Option.Some(11), Option.Some(12) }, 33)),
        };

        [TestCaseSource(nameof(Cases))]
        public void Construct_Cases(int argsCount, IEnumerable<SelectCase> expectedResult)
        {
            var cases = SelectCasesGenerator.Create(argsCount);

            cases.Should().BeEquivalentTo(expectedResult);
        }
    }
}
