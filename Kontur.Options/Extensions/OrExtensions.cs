using System;
using System.Diagnostics.Contracts;

namespace Kontur.Options
{
    public static class OrExtensions
    {
        public static Option<TValue> Or<TValue>(this IOptionMatchable<TValue> option, Func<IOptionMatchable<TValue>> onNoneFactory)
        {
            return option.Match(() => onNoneFactory().Upcast(), Option<TValue>.Some);
        }

        [Pure]
        public static Option<TValue> Or<TValue>(this IOptionMatchable<TValue> option, IOptionMatchable<TValue> onNone)
        {
            return option.Or(() => onNone);
        }
    }
}