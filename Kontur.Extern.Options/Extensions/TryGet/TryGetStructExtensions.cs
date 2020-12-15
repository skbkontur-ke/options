using System.Diagnostics.Contracts;

namespace Kontur.Extern.Options
{
    public static class TryGetStructExtensions
    {
        [Pure]
        public static bool TryGet<TValue>(
            this Option<TValue> option,
            out TValue value)
            where TValue : struct
        {
            bool result;
            (value, result) = option.Match(val => (val, true), (default, false));
            return result;
        }

        [Pure]
        public static bool TryGet<TValue>(
            this Option<TValue?> option,
            out TValue? value)
            where TValue : struct
        {
            bool result;
            (value, result) = option.Match(val => (val, true), (null, false));
            return result;
        }
    }
}