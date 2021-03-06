﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Conversion.Combinations.Linq.Plain.Where.Tasks.Select
{
    [TestFixture]
    internal class Value_Should
    {
        private static readonly IEnumerable<TestCaseData> Cases = WhereCaseGenerator.Create(1);

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> OneOption(Option<int> option, IsSuitable isSuitable)
        {
            return
                from value in option
                where Task.FromResult(isSuitable(value))
                select value;
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> TaskOption(Option<int> option, IsSuitable isSuitable)
        {
            return
                from value in Task.FromResult(option)
                where Task.FromResult(isSuitable(value))
                select value;
        }
    }
}
