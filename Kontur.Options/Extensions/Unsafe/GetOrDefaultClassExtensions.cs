using System.Diagnostics.Contracts;

namespace Kontur.Options.Unsafe
{
    public static class GetOrDefaultClassExtensions
    {
        [Pure]
#if NETSTANDARD2_0
        public static TValue? GetOrDefault<TValue>(this Option<TValue> option)
            where TValue : class
        {
            return option.Match(value => value, default(TValue));
#else
        [return: System.Diagnostics.CodeAnalysis.MaybeNull]
        public static TValue GetOrDefault<TValue>(this Option<TValue> option)
            where TValue : class?
        {
            return option.TryGet(out var result) ? result : default;
#endif
        }
    }
}