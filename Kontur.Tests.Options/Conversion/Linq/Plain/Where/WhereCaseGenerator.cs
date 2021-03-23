using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Kontur.Tests.Options.Conversion.Linq.Plain.Where
{
    internal static class WhereCaseGenerator
    {
        internal static IEnumerable<TestCaseData> Create(int argumentsCount)
        {
            return SelectCasesGenerator.Create(argumentsCount).SelectMany(CreateCases);
        }

        private static IEnumerable<TestCaseData> CreateCases(SelectCase testCase)
        {
            return WhereCaseFactory.Create(testCase.Args, testCase.Result);
        }
    }
}
