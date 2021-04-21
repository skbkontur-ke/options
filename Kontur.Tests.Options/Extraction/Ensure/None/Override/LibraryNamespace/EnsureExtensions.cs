using System;
using Kontur.Options;
using Kontur.Options.Unsafe;

namespace Kontur.Tests.Options.Extraction.Ensure.None.Override.LibraryNamespace
{
    internal static class EnsureExtensions
    {
        public static void EnsureNone(this Option<CustomValue> option)
        {
            option.EnsureNone(new Exception(Common.ExceptionMessage));
        }
    }
}