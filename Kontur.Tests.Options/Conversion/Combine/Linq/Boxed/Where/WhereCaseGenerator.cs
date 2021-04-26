using System;
using System.Collections.Generic;
using System.Linq;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Conversion.Combine.Linq.Boxed.Where
{
    internal static class WhereCaseGenerator
    {
        internal static IEnumerable<TestCaseData> CreateWhereCases<TFixtureCase>(
            this TFixtureCase fixtureCase,
            int constant,
            int argumentsCount)
            where TFixtureCase : IFixtureCase, new()
        {
            return SelectCasesGenerator
                .Create(argumentsCount)
                .SelectMany(testCase => CreateCases(testCase, value => fixtureCase.GetOption(value, constant)));
        }

        private static IEnumerable<TestCaseData> CreateCases(SelectCase testCase, Func<int, Option<int>> resultFactory)
        {
            var result = testCase.Result.Select(resultFactory);
            return WhereCaseFactory.Create(testCase.Args, result);
        }
    }
}
