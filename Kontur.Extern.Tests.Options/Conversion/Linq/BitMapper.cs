using System.Collections.Generic;
using System.Linq;

namespace Kontur.Extern.Tests.Options.Conversion.Linq
{
    internal static class BitMapper
    {
        internal static IEnumerable<IEnumerable<bool>> Get(int length)
        {
            return Enumerable.Range(1, length)
                .SelectMany(falseCount => Get(length, falseCount));
        }

        private static IEnumerable<IEnumerable<bool>> Get(int itemsCount, int falseCount)
        {
            if (falseCount == itemsCount)
            {
                return new[] { Enumerable.Repeat(false, itemsCount) };
            }

            if (itemsCount == 0)
            {
                return Enumerable.Empty<IEnumerable<bool>>();
            }

            return Get(itemsCount - 1, falseCount)
                .Select(map => map.Append(true))
                .Concat(Get(itemsCount - 1, falseCount - 1)
                    .Select(map => map.Append(false)));
        }
    }
}