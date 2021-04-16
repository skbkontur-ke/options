using System;

namespace Kontur.Options.Unsafe
{
    public static class EnsureEnsureNoneExtensions
    {
        public static void EnsureNone<TValue>(this Option<TValue> option)
        {
            option.OnSome(() => throw new InvalidOperationException($"{option} has value"));
        }
    }
}