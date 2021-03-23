using System.Collections.Generic;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Conversion.Linq.Plain.Where
{
    internal static class SelectCaseToNoneTestCasesExtensions
    {
        internal static IEnumerable<TestCaseData> ToNoneTestCases(this IEnumerable<SelectCase> cases)
        {
            return cases.ToTestCases(_ => Option<int>.None());
        }
    }
}
