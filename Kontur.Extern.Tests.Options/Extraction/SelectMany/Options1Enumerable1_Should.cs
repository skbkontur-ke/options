using System.Collections.Generic;
using System.Linq;
using Kontur.Extern.Options;
using NUnit.Framework;

namespace Kontur.Extern.Tests.Options.Extraction.SelectMany
{
    [TestFixture]
    internal class Options1Enumerable1_Should
    {
        private static readonly Option<int> None = Option.None();
        private static readonly IEnumerable<int> Empty = Enumerable.Empty<int>();

        private static TestCaseData Create(
            Option<int> option,
            IEnumerable<int> enumerable,
            IEnumerable<int> result)
        {
            return new TestCaseData(option, enumerable).Returns(result);
        }

        private static readonly TestCaseData[] Cases =
        {
            Create(None, Empty, Empty),
            Create(None, new[] { 3 }, Empty),
            Create(None, new[] { 3, 11 }, Empty),
            Create(4, Empty, Empty),
            Create(4, new[] { 3 }, new[] { 7 }),
            Create(4, new[] { 3, 11 }, new[] { 7, 15 }),
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