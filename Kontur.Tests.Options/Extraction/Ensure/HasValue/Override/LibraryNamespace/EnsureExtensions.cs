using System;
using Kontur.Options;
using Kontur.Options.Unsafe;

namespace Kontur.Tests.Options.Extraction.Ensure.HasValue.Override.LibraryNamespace
{
    internal static class EnsureExtensions
    {
        public static void EnsureHasValue(this Option<CustomValue> option)
        {
            option.EnsureHasValue(() => new Exception(Common.ExceptionMessage));
        }
    }
}