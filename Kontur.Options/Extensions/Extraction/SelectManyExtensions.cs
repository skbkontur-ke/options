using System;
using System.Collections.Generic;
using System.Linq;

namespace Kontur.Options
{
    public static class SelectManyExtensions
    {
        public static IEnumerable<TResult> SelectMany<TValue, TItem, TResult>(
            this IOption<TValue> option,
            Func<TValue, IEnumerable<TItem>> collectionSelector,
            Func<TValue, TItem, TResult> resultSelector)
        {
            return option.GetValues().SelectMany(collectionSelector, resultSelector);
        }

        public static IEnumerable<TResult> SelectMany<TItem, TValue, TResult>(
            this IEnumerable<TItem> collection,
            Func<TItem, IOption<TValue>> optionSelector,
            Func<TItem, TValue, TResult> resultSelector)
        {
            return collection.SelectMany(value => optionSelector(value).GetValues(), resultSelector);
        }
    }
}
