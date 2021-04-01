using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Kontur.Options
{
    public static class EnumerableExtensions
    {
        [Pure]
        public static IEnumerator<TValue> GetEnumerator<TValue>(this Option<TValue> option)
        {
            return option.GetValues().GetEnumerator();
        }

        public static IEnumerable<TResult> SelectMany<TValue, TItem, TResult>(
            this Option<TValue> option,
            Func<TValue, IEnumerable<TItem>> collectionSelector,
            Func<TValue, TItem, TResult> resultSelector)
        {
            return option.GetValues().SelectMany(collectionSelector, resultSelector);
        }

        public static IEnumerable<TResult> SelectMany<TItem, TValue, TResult>(
            this IEnumerable<TItem> collection,
            Func<TItem, Option<TValue>> optionSelector,
            Func<TItem, TValue, TResult> resultSelector)
        {
            return collection.SelectMany(value => optionSelector(value).GetValues(), resultSelector);
        }
    }
}
