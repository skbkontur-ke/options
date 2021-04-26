using System;
using System.Threading.Tasks;

namespace Kontur.Options
{
    internal static class FunctionResultToOption
    {
        internal static Func<TValue1, TValue2, Option<TResult>> Wrap<TValue1, TValue2, TResult>(
            Func<TValue1, TValue2, TResult> resultSelector)
        {
            return (value1, value2) => Option<TResult>.Some(resultSelector(value1, value2));
        }

        internal static Func<TValue1, TValue2, Task<Option<TResult>>> Wrap<TValue1, TValue2, TResult>(
            Func<TValue1, TValue2, Task<TResult>> resultSelector)
        {
            return async (value1, value2) => Option<TResult>.Some(await resultSelector(value1, value2).ConfigureAwait(false));
        }
    }
}
