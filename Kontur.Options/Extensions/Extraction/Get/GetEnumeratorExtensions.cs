using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Kontur.Options
{
    public static class GetEnumeratorExtensions
    {
        [Pure]
        public static IEnumerator<TValue> GetEnumerator<TValue>(this IOption<TValue> option)
        {
            return option.GetValues().GetEnumerator();
        }
    }
}
