using System;
using System.Diagnostics.Contracts;

namespace Kontur.Options.Unsafe
{
    public static class GetOrThrowExtensions
    {
        public static TValue GetOrThrow<TValue>(this Option<TValue> option, Func<Exception> exceptionFactory)
        {
            return option.Match(value => value, () => throw exceptionFactory());
        }

        [Pure]
        public static TValue GetOrThrow<TValue>(this Option<TValue> option, Exception exception)
        {
            return option.GetOrThrow(() => exception);
        }

        [Pure]
        public static TValue GetOrThrow<TValue>(this Option<TValue> option)
        {
            return option.GetOrThrow(new ValueMissingException($"Can not get value from {option}"));
        }
    }
}