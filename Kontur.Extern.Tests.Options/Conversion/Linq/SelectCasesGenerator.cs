using System.Collections.Generic;
using System.Linq;
using Kontur.Extern.Options;

namespace Kontur.Extern.Tests.Options.Conversion.Linq
{
    internal static class SelectCasesGenerator
    {
        internal static IEnumerable<SelectCase> Create(int argumentsCount)
        {
            var terms = Enumerable.Range(10, argumentsCount).ToArray();

            var someCase = new SelectCase(terms.Select(Option.Some), terms.Sum());

            return GetNoneCases(terms)
                .Select(n => new SelectCase(n, Option.None()))
                .Append(someCase);
        }

        private static IEnumerable<IEnumerable<Option<int>>> GetNoneCases(IReadOnlyCollection<int> items)
        {
            var maps = BitMapper.Get(items.Count);
            return maps
                .Select(map => map.Zip(
                    items,
                    (isSome, element) => isSome ? Option<int>.Some(element) : Option<int>.None()));
        }
    }
}
