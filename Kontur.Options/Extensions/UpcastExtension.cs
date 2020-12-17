using System.Diagnostics.Contracts;

namespace Kontur.Options
{
    public static class UpcastExtension
    {
        [Pure]
        public static Option<TResult> Upcast<TResult>(this IOptionMatch<TResult> option)
        {
            return option.Match(Option<TResult>.Some, Option<TResult>.None);
        }
    }
}
