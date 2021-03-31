using System;
using System.Diagnostics.Contracts;

namespace Kontur.Options
{
    public static class OrExtensions
    {
        public static Option<TValue> Or<TValue>(this IOptionMatchable<TValue> option, Func<Option<TValue>> onNoneFactory)
        {
            return option.Match(onNoneFactory, Option<TValue>.Some);
        }

        [Pure]
        public static Option<TValue> Or<TValue>(this IOptionMatchable<TValue> option, Option<TValue> onNone)
        {
            return option.Or(() => onNone);
        }
    }
}