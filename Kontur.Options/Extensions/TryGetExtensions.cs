using System.Diagnostics.Contracts;

namespace Kontur.Options
{
    public static class TryGetExtensions
    {
        [Pure]
        public static bool TryGet<TValue>(this Option<TValue> option, out TValue? value)
        {
            bool result;
            (value, result) = option.Match((default(TValue), false), v => (v, true));
            return result;
        }
    }
}
