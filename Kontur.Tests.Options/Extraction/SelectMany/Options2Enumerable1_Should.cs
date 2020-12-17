using System.Collections.Generic;
using System.Linq;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Extraction.SelectMany
{
    [TestFixture]
    internal class Options2Enumerable1_Should
    {
        private static readonly Option<int> None = Option.None();
        private static readonly IEnumerable<int> Empty = Enumerable.Empty<int>();

        private static TestCaseData Create(
            Option<int> option1,
            Option<int> option2,
            IEnumerable<int> enumerable,
            IEnumerable<int> result)
        {
            return new TestCaseData(option1, option2, enumerable).Returns(result);
        }

        private static readonly TestCaseData[] Cases =
        {
            Create(None, None, Empty, Empty),
            Create(None, None, new[] { 3 }, Empty),
            Create(None, None, new[] { 3, 11 }, Empty),
            Create(None, 17, Empty, Empty),
            Create(None, 17, new[] { 3 }, Empty),
            Create(None, 17, new[] { 3, 11 }, Empty),
            Create(4, None, Empty, Empty),
            Create(4, None, new[] { 3 }, Empty),
            Create(4, None, new[] { 3, 11 }, Empty),
            Create(4, 17, Empty, Empty),
            Create(4, 17, new[] { 3 }, new[] { 24 }),
            Create(4, 17, new[] { 3, 11 }, new[] { 24, 32 }),
        };

        [TestCaseSource(nameof(Cases))]
        public IEnumerable<int> Option_Option_Enumerable(
            Option<int> option1,
            Option<int> option2,
            IEnumerable<int> enumerable)
        {
            return
                from x in option1
                from y in option2
                from z in enumerable
                select x + y + z;
        }

        [TestCaseSource(nameof(Cases))]
        public IEnumerable<int> Option_Enumerable_Option(
            Option<int> option1,
            Option<int> option2,
            IEnumerable<int> enumerable)
        {
            return
                from x in option1
                from y in enumerable
                from z in option2
                select x + y + z;
        }

        [TestCaseSource(nameof(Cases))]
        public IEnumerable<int> Enumerable_Option_Option(
            Option<int> option1,
            Option<int> option2,
            IEnumerable<int> enumerable)
        {
            return
                from x in enumerable
                from y in option1
                from z in option2
                select x + y + z;
        }
    }
}