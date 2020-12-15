using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Kontur.Extern.Tests.Options.Conversion.Linq.SelectMany.Options3
{
    internal static class Common
    {
        internal static readonly IEnumerable<SelectCase> Cases = SelectCasesGenerator.Create(3);

        internal static readonly IEnumerable<TestCaseData> ResultCases =
            Cases.Select(testCase => new TestCaseData(testCase.Args).Returns(testCase.Result));
    }
}
