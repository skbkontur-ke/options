using System.Collections.Generic;
using System.Threading.Tasks;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Conversion.Linq.SelectMany.Options2.Tasks1
{
    [TestFixture]
    internal class None_Should
    {
        private static readonly Option<int> None = Option.None();

        private static readonly Task<int> Task1000 = Common.Task1000;

        private static readonly IEnumerable<TestCaseData> Cases = Options2Common.NoneCases;

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> Option_Option_Task(
            Option<int> option1,
            Option<int> option2)
        {
            return
                from x in option1
                from y in option2
                from z in Task1000
                select None;
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> TaskOption_Option_Task(
            Option<int> option1,
            Option<int> option2)
        {
            return
                from x in Task.FromResult(option1)
                from y in option2
                from z in Task1000
                select None;
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> Option_TaskOption_Task(
            Option<int> option1,
            Option<int> option2)
        {
            return
                from x in option1
                from y in Task.FromResult(option2)
                from z in Task1000
                select None;
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> TaskOption_TaskOption_Task(
            Option<int> option1,
            Option<int> option2)
        {
            return
                from x in Task.FromResult(option1)
                from y in Task.FromResult(option2)
                from z in Task1000
                select None;
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> Option_Task_Option(Option<int> option1, Option<int> option2)
        {
            return
                from x in option1
                from y in Task1000
                from z in option2
                select None;
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> TaskOption_Task_Option(Option<int> option1, Option<int> option2)
        {
            return
                from x in Task.FromResult(option1)
                from y in Task1000
                from z in option2
                select None;
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> Option_Task_TaskOption(Option<int> option1, Option<int> option2)
        {
            return
                from x in option1
                from y in Task1000
                from z in Task.FromResult(option2)
                select None;
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> TaskOption_Task_TaskOption(Option<int> option1, Option<int> option2)
        {
            return
                from x in Task.FromResult(option1)
                from y in Task1000
                from z in Task.FromResult(option2)
                select None;
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> Task_Option_Option(Option<int> option1, Option<int> option2)
        {
            return
                from x in Task1000
                from y in option1
                from z in option2
                select None;
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> Task_TaskOption_Option(Option<int> option1, Option<int> option2)
        {
            return
                from x in Task1000
                from y in Task.FromResult(option1)
                from z in option2
                select None;
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> Task_Option_TaskOption(Option<int> option1, Option<int> option2)
        {
            return
                from x in Task1000
                from y in option1
                from z in Task.FromResult(option2)
                select None;
        }

        [TestCaseSource(nameof(Cases))]
        public Task<Option<int>> Task_TaskOption_TaskOption(Option<int> option1, Option<int> option2)
        {
            return
                from x in Task1000
                from y in Task.FromResult(option1)
                from z in Task.FromResult(option2)
                select None;
        }
    }
}