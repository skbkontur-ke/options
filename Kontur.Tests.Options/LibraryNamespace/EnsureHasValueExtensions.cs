using Kontur.Options;

namespace Kontur.Tests.Options.LibraryNamespace
{
    internal static class EnsureHasValueExtensions
    {
        public static void EnsureHasValue(this Option<CustomValue> option)
        {
            option.EnsureHasValue(LibraryException.Instance);
        }
    }
}