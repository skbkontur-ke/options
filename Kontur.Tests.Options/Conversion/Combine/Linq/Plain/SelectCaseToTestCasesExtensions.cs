using System.Collections.Generic;
using NUnit.Framework;

namespace Kontur.Tests.Options.Conversion.Combine.Linq.Plain
{
    internal static class SelectCaseToTestCasesExtensions
    {
        internal static IEnumerable<TestCaseData> ToTestCases(this IEnumerable<SelectCase> cases)
        {
            return cases.ToTestCases(option => option);
        }
    }
}
