using System.Collections.Generic;
using System.Linq;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Extraction.SelectMany
{
    [TestFixture]
    internal class Options1Enumerable1_Should
    {
        private static readonly Option<int> None = Option<int>.None();
        private static readonly Option<int> Some4 = Option<int>.Some(4);
        private static readonly IEnumerable<int> Empty = Enumerable.Empty<int>();

        private static TestCaseData Create(
            Option<int> option,
            IEnumerable<int> enumerable,
            IEnumerable<int> result)
        {
            return new(option, enumerable) { ExpectedResult = result };
        }

        private static readonly TestCaseData[] Cases =
        {
            Create(None, Empty, Empty),
            Create(None, new[] { 3 }, Empty),
            Create(None, new[] { 3, 11 }, Empty),
            Create(Some4, Empty, Empty),
            Create(Some4, new[] { 3 }, new[] { 7 }),
            Create(Some4, new[] { 3, 11 }, new[] { 7, 15 }),
        };

        [TestCaseSource(nameof(Cases))]
        public IEnumerable<int> Option_Enumerable(Option<int> option, IEnumerable<int> enumerable)
        {
            return
                from x in option
                from y in enumerable
                select x + y;
        }

        [TestCaseSource(nameof(Cases))]
        public IEnumerable<int> Enumerable_Option(Option<int> option, IEnumerable<int> enumerable)
        {
            return
                from x in enumerable
                from y in option
                select x + y;
        }
    }
}