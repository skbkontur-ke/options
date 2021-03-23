using System;
using System.Collections.Generic;
using System.Linq;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Conversion.Linq
{
    internal static class SelectCaseToTestCasesExtensions
    {
        internal static IEnumerable<TestCaseData> ToTestCases<TResult>(
            this IEnumerable<SelectCase> cases,
            Func<Option<int>, TResult> resultSelector)
        {
            return cases.Select(testCase => new TestCaseData(testCase.Args).Returns(resultSelector(testCase.Result)));
        }

        internal static IEnumerable<TestCaseData> ToTestCases(this IEnumerable<SelectCase> cases)
        {
            return cases.ToTestCases(option => option);
        }
    }
}
