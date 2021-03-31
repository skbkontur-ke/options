using System.Diagnostics.Contracts;

namespace Kontur.Options.Unsafe
{
    public static class GetOrDefaultExtensions
    {
        [Pure]
        public static TValue? GetOrDefault<TValue>(this IOptionMatchable<TValue> option)
        {
            return option.GetOrElse(default(TValue));
        }
    }
}