using System.Collections.Generic;
using System.Linq;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Extraction.SelectMany
{
    [TestFixture]
    internal class Options1Enumerable2_Should
    {
        private static readonly Option<int> None = Option<int>.None();
        private static readonly Option<int> Some4 = Option<int>.Some(4);
        private static readonly IEnumerable<int> Empty = Enumerable.Empty<int>();

        private static TestCaseData Create(
            Option<int> option,
            IEnumerable<int> enumerable1,
            IEnumerable<int> enumerable2,
            IEnumerable<int> result)
        {
            return new(option, enumerable1, enumerable2) { ExpectedResult = result };
        }

        private static readonly TestCaseData[] Cases =
        {
            Create(None, Empty, Empty, Empty),
            Create(None, Empty, new[] { 17 }, Empty),
            Create(None, Empty, new[] { 17, 29 }, Empty),
            Create(None, new[] { 3 }, Empty, Empty),
            Create(None, new[] { 3 }, new[] { 17 }, Empty),
            Create(None, new[] { 3 }, new[] { 17, 29 }, Empty),
            Create(None, new[] { 3, 11 }, Empty, Empty),
            Create(None, new[] { 3, 11 }, new[] { 17 }, Empty),
            Create(None, new[] { 3, 11 }, new[] { 17, 29 }, Empty),
            Create(Some4, Empty, Empty, Empty),
            Create(Some4, Empty, new[] { 17 }, Empty),
            Create(Some4, Empty, new[] { 17, 29 }, Empty),
            Create(Some4, new[] { 3 }, Empty, Empty),
            Create(Some4, new[] { 3 }, new[] { 17 }, new[] { 24 }),
            Create(Some4, new[] { 3 }, new[] { 17, 29 }, new[] { 24, 36 }),
            Create(Some4, new[] { 3, 11 }, Empty, Empty),
            Create(Some4, new[] { 3, 11 }, new[] { 17 }, new[] { 24, 32 }),
            Create(Some4, new[] { 3, 11 }, new[] { 17, 29 }, new[] { 24, 36, 32, 44 }),
        };

        [TestCaseSource(nameof(Cases))]
        public IEnumerable<int> Option_Enumerable_Enumerable(
            Option<int> option,
            IEnumerable<int> enumerable1,
            IEnumerable<int> enumerable2)
        {
            return
                from x in option
                from y in enumerable1
                from z in enumerable2
                select x + y + z;
        }

        [TestCaseSource(nameof(Cases))]
        public IEnumerable<int> Enumerable_Option_Enumerable(
            Option<int> option,
            IEnumerable<int> enumerable1,
            IEnumerable<int> enumerable2)
        {
            return
                from x in enumerable1
                from y in option
                from z in enumerable2
                select x + y + z;
        }

        [TestCaseSource(nameof(Cases))]
        public IEnumerable<int> Enumerable_Enumerable_Option(
            Option<int> option,
            IEnumerable<int> enumerable1,
            IEnumerable<int> enumerable2)
        {
            return
                from x in enumerable1
                from y in enumerable2
                from z in option
                select x + y + z;
        }
    }
}