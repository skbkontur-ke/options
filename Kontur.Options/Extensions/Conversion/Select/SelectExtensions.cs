using System;

namespace Kontur.Options
{
    public static class SelectExtensions
    {
        public static Option<TResult> Select<TValue, TResult>(
            this Option<TValue> option,
            Func<TValue, Option<TResult>> resultSelector)
        {
            return option.Match(Option<TResult>.None, resultSelector);
        }
    }
}