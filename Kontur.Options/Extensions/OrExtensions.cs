using System;
using System.Diagnostics.Contracts;

namespace Kontur.Options
{
    public static class OrExtensions
    {
        public static Option<TValue> Or<TValue>(this IOptionMatch<TValue> option, Func<Option<TValue>> onNoneFactory)
        {
            return option.Match(onNoneFactory, Option.Some);
        }

        [Pure]
        public static Option<TValue> Or<TValue>(this IOptionMatch<TValue> option, Option<TValue> onNone)
        {
            return option.Or(() => onNone);
        }
    }
}