using System.Collections.Generic;
using System.Linq;
using Kontur.Extern.Options;
using NUnit.Framework;

namespace Kontur.Extern.Tests.Options.Conversion.Linq.SelectMany.Options1
{
    internal static class Options1Common
    {
        internal static readonly IEnumerable<SelectCase> Cases = SelectCasesGenerator.Create(1);

        internal static readonly IEnumerable<TestCaseData> NoneCases = Cases
            .Select(testCase => new TestCaseData(testCase.Args).Returns(Option.None<int>()));
    }
}
