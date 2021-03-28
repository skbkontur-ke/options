using System.Collections.Generic;
using System.Linq;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Equality
{
    [TestFixture]
    internal class Equals_Should
    {
        private static readonly IEnumerable<TestCaseData> Cases =
            from pair in new[]
                {
                    (Cases: Common.CreateNonEqualsCases(), Result: false),
                    (Cases: Common.CreateEqualsCases(), Result: true),
                }
            from testCase in pair.Cases
            select testCase.Returns(pair.Result);

        [TestCaseSource(nameof(Cases))]
        public bool Compare<TValue1, TValue2>(Option<TValue1> option1, Option<TValue2> option2)
        {
            return option1.Equals(option2);
        }
    }
}