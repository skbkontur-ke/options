using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Kontur.Options
{
    public static class GetValuesExtensions
    {
        [Pure]
        public static IEnumerable<TValue> GetValues<TValue>(this IOptionMatchable<TValue> option)
        {
            return option.Match(Enumerable.Empty<TValue>, value => new[] { value });
        }
    }
}
