using System;
using System.Collections.Generic;
using System.Linq;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Conversion.Linq
{
    internal static class SelectCaseToTestCasesExtensions
    {
        internal static IEnumerable<TestCaseData> ToTestCases(
            this IEnumerable<SelectCase> cases,
            Func<Option<int>, Option<int>> resultSelector)
        {
            return cases.Select(testCase => new TestCaseData(testCase.Args.Cast<object>().ToArray()).Returns(resultSelector(testCase.Result)));
        }
    }
}
