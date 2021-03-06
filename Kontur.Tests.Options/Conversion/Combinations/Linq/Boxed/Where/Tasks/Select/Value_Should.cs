﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Conversion.Combinations.Linq.Boxed.Where.Tasks.Select
{
    internal class Value_Should<TFixtureCase> : LinqTestBase<TFixtureCase>
        where TFixtureCase : IFixtureCase, new()
    {
        private static readonly IEnumerable<TestCaseData> Cases = FixtureCase.CreateWhereCases(Constant, 1);

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> OneOption(Option<int> option, IsSuitable isSuitable)
        {
            return
                from value in option
                where Task.FromResult(isSuitable(value))
                select GetOption(value);
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> TaskOption(Option<int> option, IsSuitable isSuitable)
        {
            return
                from value in Task.FromResult(option)
                where Task.FromResult(isSuitable(value))
                select GetOption(value);
        }
    }
}
