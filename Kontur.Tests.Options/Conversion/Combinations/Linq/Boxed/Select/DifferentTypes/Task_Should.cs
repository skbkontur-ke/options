﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Conversion.Combinations.Linq.Boxed.Select.DifferentTypes
{
    internal class Task_Should<TFixtureCase> : LinqTestBase<TFixtureCase>
        where TFixtureCase : IFixtureCase, new()
    {
        private static readonly IEnumerable<TestCaseData> Cases = CreateSelectCases(1);

        private static Task<Option<string>> SelectResult(int value)
        {
            return Task.FromResult(GetOption(ConvertToString(value)));
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<string>> OneOption(Option<int> option)
        {
            return
                from value in option
                select SelectResult(value);
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<string>> Option_Let(Option<int> option)
        {
            return
                from valueLet in option
                let value = valueLet
                select SelectResult(value);
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<string>> TaskOption(Option<int> option)
        {
            return
                from value in Task.FromResult(option)
                select SelectResult(value);
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<string>> TaskOption_Let(Option<int> option)
        {
            return
                from valueLet in Task.FromResult(option)
                let value = valueLet
                select SelectResult(value);
        }
    }
}
