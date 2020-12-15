using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace Kontur.Extern.Tests.Options.Conversion.Linq
{
    [TestFixture]
    internal class BitMapper_Should
    {
        private static TestCaseData Create(int length, params IEnumerable<bool>[] expectedResult)
        {
            return new TestCaseData(length, expectedResult);
        }

        private static readonly TestCaseData[] Cases =
        {
            Create(0),
            Create(1, new[] { false }),
            Create(2, new[] { false, false }, new[] { false, true }, new[] { true, false }),
            Create(
                3,
                new[] { false, false, false },
                new[] { false, false, true },
                new[] { false, true, false },
                new[] { false, true, true },
                new[] { true, false, false },
                new[] { true, false, true },
                new[] { true, true, false }),
        };

        [TestCaseSource(nameof(Cases))]
        public void Construct_Map(int length, IEnumerable<IEnumerable<bool>> expectedResult)
        {
            var result = BitMapper.Get(length);

            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}
