using System.Diagnostics.Contracts;

namespace Kontur.Options
{
    public static class TryGetClassExtensions
    {
        [Pure]
        public static bool TryGet<TValue>(this Option<TValue> option, out TValue? value)
            where TValue : class
        {
            bool result;
            (value, result) = option.Match(
                v => (v, true),
                (default(TValue?), false));
            return result;
        }
    }
}