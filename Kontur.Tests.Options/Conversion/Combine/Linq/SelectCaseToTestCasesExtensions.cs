using System;
using System.Collections.Generic;
using System.Linq;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Conversion.Combine.Linq
{
    internal static class SelectCaseToTestCasesExtensions
    {
        internal static IEnumerable<TestCaseData> ToTestCases<T>(
            this IEnumerable<SelectCase> cases,
            Func<Option<int>, Option<T>> resultSelector)
        {
            return cases.Select(
                testCase => new TestCaseData(testCase.Args.Cast<object>().ToArray())
                    .Returns(resultSelector(testCase.Result)));
        }
    }
}
