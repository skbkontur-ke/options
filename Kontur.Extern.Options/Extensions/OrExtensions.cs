using System;
using System.Diagnostics.Contracts;

namespace Kontur.Extern.Options
{
    public static class OrExtensions
    {
        public static Option<TValue> Or<TValue>(this IOptionMatch<TValue> option, Func<Option<TValue>> onNoneFactory)
        {
            return option.Match(Option.Some, onNoneFactory);
        }

        [Pure]
        public static Option<TValue> Or<TValue>(this IOptionMatch<TValue> option, Option<TValue> onNone)
        {
            return option.Or(() => onNone);
        }
    }
}