using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Kontur.Extern.Tests.Options.Conversion.Linq.SelectMany.Options2.Plain
{
    internal static class Common
    {
        internal static readonly IEnumerable<TestCaseData> ResultCases =
            Options2Common.Cases.Select(testCase => new TestCaseData(testCase.Args).Returns(testCase.Result));
    }
}
