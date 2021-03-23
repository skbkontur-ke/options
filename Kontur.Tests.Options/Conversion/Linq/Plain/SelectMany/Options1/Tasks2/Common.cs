using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Kontur.Tests.Options.Conversion.Linq.Plain.SelectMany.Options1.Tasks2
{
    internal static class Common
    {
        private const int TaskTerm1 = 1000;
        private const int TaskTerm2 = 10000;

        internal static readonly Task<int> Task1000 = Task.FromResult(TaskTerm1);
        internal static readonly Task<int> Task10000 = Task.FromResult(TaskTerm2);

        internal static readonly IEnumerable<TestCaseData> Cases = SelectCasesGenerator.Create(1)
            .ToTestCases(option => option.Map(sum => sum + TaskTerm1 + TaskTerm2));
    }
}
