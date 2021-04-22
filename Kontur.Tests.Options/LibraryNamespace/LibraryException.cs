using System;

namespace Kontur.Tests.Options.LibraryNamespace
{
    internal static class LibraryException
    {
        internal const string Message = "Overriden";

        internal static Exception Instance => new(Message);
    }
}
