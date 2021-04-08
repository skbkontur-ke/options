using System.Collections.Generic;
using System.Globalization;
using NUnit.Framework;

namespace Kontur.Tests.Options.Conversion.Linq.Plain.Select.DifferentTypes
{
    internal static class SelectCaseToDifferentTypeTestCasesExtensions
    {
        internal static IEnumerable<TestCaseData> ToDifferentTypeTestCases(this IEnumerable<SelectCase> cases)
        {
            return cases.ToTestCases(result => result.Map(ConvertToString));
        }

        internal static string ConvertToString(int value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }
    }
}
