using System;

namespace Kontur.Options.Unsafe
{
    public static class EnsureHasValueExtensions
    {
        public static void EnsureHasValue<TValue>(this Option<TValue> option)
        {
            option.OnNone(() => throw new InvalidOperationException($"No value in {option}"));
        }
    }
}