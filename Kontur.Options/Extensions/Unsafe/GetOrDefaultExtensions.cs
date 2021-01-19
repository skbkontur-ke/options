using System.Diagnostics.Contracts;

namespace Kontur.Options.Unsafe
{
    public static class GetOrDefaultExtensions
    {
        [Pure]
        public static TValue? GetOrDefault<TValue>(this Option<TValue> option)
        {
            return option.Match(default(TValue), value => value);
        }
    }
}