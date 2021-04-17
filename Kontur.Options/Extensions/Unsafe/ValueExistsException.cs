using System;

namespace Kontur.Options.Unsafe
{
    public sealed class ValueExistsException : InvalidOperationException
    {
        internal ValueExistsException(string message)
            : base(message)
        {
        }
    }
}
