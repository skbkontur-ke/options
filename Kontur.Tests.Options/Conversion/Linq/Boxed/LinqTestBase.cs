using System;
using System.Collections.Generic;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Conversion.Linq.Boxed
{
    [TestFixture(typeof(NoneFixtureCase))]
    [TestFixture(typeof(SomeFixtureCase))]
    [TestFixture(typeof(SomeConstantFixtureCase))]
    internal abstract class LinqTestBase<TFixtureCase>
        where TFixtureCase : IFixtureCase, new()
    {
        private protected LinqTestBase()
        {
        }

        private static readonly TFixtureCase FixtureCase = new();

        protected static Option<int> GetOption(int value) => FixtureCase.GetResult(value);

        protected static IEnumerable<TestCaseData> GenerateCases(int argumentsCount, Func<int, int> convertValue)
        {
            return SelectCasesGenerator
                .Create(argumentsCount)
                .ToTestCases(result => result.Select(
                    value => GetOption(convertValue(value))));
        }
    }
}
