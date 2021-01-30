using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Kontur.Tests.Options.Conversion.Linq.SelectMany.Options1.Tasks2
{
    internal static class Common
    {
        private const int TaskTerm1 = 1000;
        private const int TaskTerm2 = 10000;

        internal static readonly Task<int> Task1000 = Task.FromResult(TaskTerm1);
        internal static readonly Task<int> Task10000 = Task.FromResult(TaskTerm2);

        internal static readonly IEnumerable<TestCaseData> Cases = Options1Common.Cases
            .Select(testCase => new TestCaseData(testCase.Args)
                .Returns(testCase.Result.Select(sum => sum + TaskTerm1 + TaskTerm2)));
    }
}
