using System;

namespace Kontur.Options
{
    public sealed class ValueMissingException : InvalidOperationException
    {
        internal ValueMissingException(string message)
            : base(message)
        {
        }
    }
}
