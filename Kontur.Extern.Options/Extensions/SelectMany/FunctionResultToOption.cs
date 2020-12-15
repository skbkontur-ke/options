using System;

namespace Kontur.Extern.Options
{
    internal static class FunctionResultToOption
    {
        internal static Func<TValue1, TValue2, Option<TResult>> Wrap<TValue1, TValue2, TResult>(
            Func<TValue1, TValue2, TResult> resultSelector)
        {
            return (value1, value2) => Option.Some(resultSelector(value1, value2));
        }
    }
}
