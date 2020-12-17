using System.Diagnostics.Contracts;

namespace Kontur.Options.Unsafe
{
    public static class GetOrDefaultStructExtensions
    {
        [Pure]
        public static TValue GetOrDefault<TValue>(this Option<TValue> option)
            where TValue : struct
        {
            return option.GetOrElse(default(TValue));
        }

        [Pure]
        public static TValue? GetOrDefault<TValue>(this Option<TValue?> option)
            where TValue : struct
        {
            return option.GetOrElse(null as TValue?);
        }
    }
}