using System.Collections.Generic;
using FluentAssertions;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Conversion.Linq
{
    [TestFixture]
    internal class SelectCaseGenerator_Should
    {
        private static readonly Option<int> None = Option<int>.None();
        private static readonly Option<int> Some10 = CreateSome(10);
        private static readonly Option<int> Some11 = CreateSome(11);
        private static readonly Option<int> Some12 = CreateSome(12);

        private static Option<int> CreateSome(int value) => Option<int>.Some(value);

        private static TestCaseData Create(int argsCount, params SelectCase[] expectedResult)
        {
            return new(argsCount, expectedResult);
        }

        private static readonly TestCaseData[] Cases =
        {
            Create(
                1,
                new SelectCase(new[] { None }, None),
                new SelectCase(new[] { Some10 }, Some10)),
            Create(
                2,
                new SelectCase(new[] { None, None }, None),
                new SelectCase(new[] { Some10, None }, None),
                new SelectCase(new[] { None, Some11 }, None),
                new SelectCase(new[] { Some10, Some11 }, CreateSome(21))),
            Create(
                3,
                new SelectCase(new[] { None, None, None }, None),
                new SelectCase(new[] { Some10, None, None }, None),
                new SelectCase(new[] { None, Some11, None }, None),
                new SelectCase(new[] { Some10, Some11, None }, None),
                new SelectCase(new[] { None, None, Some12 }, None),
                new SelectCase(new[] { Some10, None, Some12 }, None),
                new SelectCase(new[] { None, Some11, Some12 }, None),
                new SelectCase(new[] { Some10, Some11, Some12 }, CreateSome(33))),
        };

        [TestCaseSource(nameof(Cases))]
        public void Construct_Cases(int argsCount, IEnumerable<SelectCase> expectedResult)
        {
            var cases = SelectCasesGenerator.Create(argsCount);

            cases.Should().BeEquivalentTo(expectedResult);
        }
    }
}
