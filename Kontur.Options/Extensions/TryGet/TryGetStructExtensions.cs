using System.Diagnostics.Contracts;

namespace Kontur.Options
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
            (value, result) = option.Match((default, false), val => (val, true));
            return result;
        }

        [Pure]
        public static bool TryGet<TValue>(
            this Option<TValue?> option,
            out TValue? value)
            where TValue : struct
        {
            bool result;
            (value, result) = option.Match((null, false), val => (val, true));
            return result;
        }
    }
}