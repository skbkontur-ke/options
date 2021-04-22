using JetBrains.Annotations;
using Kontur.Options;
using Kontur.Options.Unsafe;

namespace Kontur.Tests.Options.LibraryNamespace
{
    internal static class GetOrThrowExtensions
    {
        [Pure]
        public static CustomValue GetOrThrow(this IOptionMatchable<CustomValue> option)
        {
            return option.GetOrThrow(LibraryException.Instance);
        }
    }
}
