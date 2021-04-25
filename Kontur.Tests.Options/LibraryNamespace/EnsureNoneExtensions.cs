using Kontur.Options;

namespace Kontur.Tests.Options.LibraryNamespace
{
    internal static class EnsureNoneExtensions
    {
        public static void EnsureNone(this Option<CustomValue> option)
        {
            option.EnsureNone(LibraryException.Instance);
        }
    }
}