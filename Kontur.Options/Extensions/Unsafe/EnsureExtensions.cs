using System;

namespace Kontur.Options.Unsafe
{
    public static class EnsureExtensions
    {
        public static void EnsureHasValue<TValue>(this Option<TValue> option)
        {
            option.OnNone(() => throw new InvalidOperationException($"No value in {option}"));
        }

        public static void EnsureNone<TValue>(this Option<TValue> option)
        {
            option.OnSome(() => throw new InvalidOperationException($"{option} has value"));
        }
    }
}