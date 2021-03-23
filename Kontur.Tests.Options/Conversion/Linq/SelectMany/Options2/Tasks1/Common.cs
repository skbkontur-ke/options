using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Kontur.Tests.Options.Conversion.Linq.SelectMany.Options2.Tasks1
{
    internal static class Common
    {
        private const int TaskTerm = 1000;

        internal static readonly Task<int> Task1000 = Task.FromResult(TaskTerm);

        internal static readonly IEnumerable<TestCaseData> Cases = SelectCasesGenerator.Create(2)
            .ToTestCases(option => option.Map(sum => sum + TaskTerm));
    }
}
