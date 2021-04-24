using System;

namespace Kontur.Options.Unsafe
{
    public sealed class ValueMissingException : InvalidOperationException
    {
        internal ValueMissingException(string message)
            : base(message)
        {
        }
    }
}
