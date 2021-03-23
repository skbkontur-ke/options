﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Conversion.Linq.Boxed.SelectMany.Options1.Tasks2
{
    [TestFixture]
    internal class Some_Should
    {
        private const int TaskTerm1 = 1000;
        private const int TaskTerm2 = 10000; 
        private static readonly Task<int> Task1000 = Task.FromResult(TaskTerm1);
        private static readonly Task<int> Task10000 = Task.FromResult(TaskTerm2);

        private static readonly IEnumerable<TestCaseData> Cases = SelectCasesGenerator.Create(1)
            .ToTestCases(option => option.Map(sum => sum + TaskTerm1 + TaskTerm2));

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> Task_Option_Task(Option<int> option)
        {
            return
                from x in Task1000
                from y in option
                from z in Task10000
                select Option.Some(x + y + z);
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> Task_TaskOption_Task(Option<int> option)
        {
            return
                from x in Task1000
                from y in Task.FromResult(option)
                from z in Task10000
                select Option.Some(x + y + z);
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> Option_Task_Task(Option<int> option)
        {
            return
                from x in option
                from y in Task1000
                from z in Task10000
                select Option.Some(x + y + z);
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> TaskOption_Task_Task(Option<int> option)
        {
            return
                from x in Task.FromResult(option)
                from y in Task1000
                from z in Task10000
                select Option.Some(x + y + z);
        }
    }
}