using System;
using System.Diagnostics.Contracts;
using Kontur.Options;
using Kontur.Options.Unsafe;

namespace Kontur.Tests.Options.Extraction.GetOrThrow.Override.LibraryNamespace
{
    internal static class GetOrThrowExtensions
    {
        [Pure]
        public static CustomValue GetOrThrow(this IOptionMatchable<CustomValue> option)
        {
            return option.GetOrThrow(() => new Exception(Common.ExceptionMessage));
        }
    }
}