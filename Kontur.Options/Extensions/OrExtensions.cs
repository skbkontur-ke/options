using System;
using System.Diagnostics.Contracts;

namespace Kontur.Options
{
    public static class OrExtensions
    {
        public static Option<TValue> Or<TValue>(this IOption<TValue> option, Func<IOption<TValue>> onNoneFactory)
        {
            return option.Match(() => onNoneFactory().Upcast(), Option<TValue>.Some);
        }

        [Pure]
        public static Option<TValue> Or<TValue>(this IOption<TValue> option, IOption<TValue> onNone)
        {
            return option.Or(() => onNone);
        }
    }
}