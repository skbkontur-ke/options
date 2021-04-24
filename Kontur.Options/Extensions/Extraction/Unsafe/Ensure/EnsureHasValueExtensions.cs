using System;

namespace Kontur.Options.Unsafe
{
    public static class EnsureHasValueExtensions
    {
        public static void EnsureHasValue<TValue>(this IOption<TValue> option, Func<Exception> exceptionFactory)
        {
            option.OnNone(() => throw exceptionFactory());
        }

        public static void EnsureHasValue<TValue>(this IOption<TValue> option, Exception exception)
        {
            option.EnsureHasValue(() => exception);
        }

        public static void EnsureHasValue<TValue>(this IOption<TValue> option)
        {
            option.EnsureHasValue(new ValueMissingException($"No value in {option}"));
        }
    }
}