using System;

namespace Kontur.Options.Unsafe
{
    public static class EnsureEnsureNoneExtensions
    {
        public static void EnsureNone<TValue>(this IOption<TValue> option, Func<TValue, Exception> exceptionFactory)
        {
            option.OnSome(value => throw exceptionFactory(value));
        }

        public static void EnsureNone<TValue>(this IOption<TValue> option, Func<Exception> exceptionFactory)
        {
            option.EnsureNone(_ => exceptionFactory());
        }

        public static void EnsureNone<TValue>(this IOption<TValue> option, Exception exception)
        {
            option.EnsureNone(() => exception);
        }

        public static void EnsureNone<TValue>(this IOption<TValue> option)
        {
            option.EnsureNone(new ValueExistsException($"{option} has value"));
        }
    }
}