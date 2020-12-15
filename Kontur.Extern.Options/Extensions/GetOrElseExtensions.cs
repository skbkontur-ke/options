using System;
using System.Diagnostics.Contracts;

namespace Kontur.Extern.Options
{
    public static class GetOrElseExtensions
    {
        public static TValue GetOrElse<TValue>(this IOptionMatch<TValue> option, Func<TValue> onNoneValueFactory)
        {
            return option.Match(value => value, onNoneValueFactory);
        }

        [Pure]
        public static TValue GetOrElse<TValue>(this IOptionMatch<TValue> option, TValue onNoneValue)
        {
            return option.GetOrElse(() => onNoneValue);
        }
    }
}