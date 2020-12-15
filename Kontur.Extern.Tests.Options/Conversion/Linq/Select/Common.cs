using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Kontur.Extern.Tests.Options.Conversion.Linq.Select
{
    internal static class Common
    {
        internal static readonly IEnumerable<SelectCase> Cases = SelectCasesGenerator.Create(1);

        internal static readonly IEnumerable<TestCaseData> ResultCases =
            Cases.Select(testCase => new TestCaseData(testCase.Args).Returns(testCase.Result));
    }
}
