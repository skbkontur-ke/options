﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Kontur.Extern.Options;
using NUnit.Framework;

namespace Kontur.Extern.Tests.Options.Conversion.Linq.Select
{
    [TestFixture]
    internal class Some_Should
    {
        private static readonly IEnumerable<TestCaseData> Cases = Common.ResultCases;

        [TestCaseSource(nameof(Cases))]
        public Option<int> Option_Some(Option<int> option)
        {
            return
                from value in option
                select Option.Some(value);
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> TaskOption_Some(Option<int> option)
        {
            return
                from value in Task.FromResult(option)
                select Option.Some(value);
        }
    }
}
