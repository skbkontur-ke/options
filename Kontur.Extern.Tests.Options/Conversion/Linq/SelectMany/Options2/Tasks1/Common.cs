﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kontur.Extern.Options;
using NUnit.Framework;

namespace Kontur.Extern.Tests.Options.Conversion.Linq.SelectMany.Options2.Tasks1
{
    internal static class Common
    {
        private const int TaskTerm = 1000;

        internal static readonly Task<int> Task1000 = Task.FromResult(TaskTerm);

        internal static readonly IEnumerable<TestCaseData> Cases = Options2Common.Cases
            .Select(testCase => new TestCaseData(testCase.Args)
                .Returns(testCase.Result.Match(
                    sum => Option.Some(sum + TaskTerm),
                    () => Option.None())));
    }
}
