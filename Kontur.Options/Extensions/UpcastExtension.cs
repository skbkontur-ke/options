using System.Diagnostics.Contracts;

namespace Kontur.Options
{
    public static class UpcastExtension
    {
        [Pure]
        public static Option<TResult> Upcast<TResult>(this IMatchable<TResult> option)
        {
            return option.Match(Option<TResult>.None, Option<TResult>.Some);
        }
    }
}
