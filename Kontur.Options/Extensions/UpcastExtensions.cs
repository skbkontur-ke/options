using System.Diagnostics.Contracts;

namespace Kontur.Options
{
    public static class UpcastExtensions
    {
        [Pure]
        public static Option<TResult> Upcast<TResult>(this IOption<TResult> option)
        {
            return option.Match(Option<TResult>.None, Option<TResult>.Some);
        }
    }
}
