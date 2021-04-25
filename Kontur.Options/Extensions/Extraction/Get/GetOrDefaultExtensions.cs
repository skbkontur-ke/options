using System.Diagnostics.Contracts;

namespace Kontur.Options
{
    public static class GetOrDefaultExtensions
    {
        [Pure]
        public static TValue? GetOrDefault<TValue>(this IOption<TValue> option)
        {
            return option.GetOrElse(default(TValue));
        }
    }
}