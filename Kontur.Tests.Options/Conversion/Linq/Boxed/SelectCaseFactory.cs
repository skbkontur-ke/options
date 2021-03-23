using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Kontur.Tests.Options.Conversion.Linq.Boxed
{
    internal static class SelectCaseFactory
    {
        internal static IEnumerable<TestCaseData> GenerateCases<TFixtureCase>(
            this TFixtureCase fixtureCase,
            int argumentsCount,
            Func<int, int> convertValue)
            where TFixtureCase : IFixtureCase, new()
        {
            return SelectCasesGenerator
                .Create(argumentsCount)
                .ToTestCases(result => result.Select(
                    value => fixtureCase.GetOption(convertValue(value))));
        }

        internal static IEnumerable<TestCaseData> GenerateCases<TFixtureCase>(this TFixtureCase fixtureCase, int argumentsCount)
            where TFixtureCase : IFixtureCase, new()
        {
            return fixtureCase.GenerateCases(argumentsCount, x => x);
        }
    }
}
