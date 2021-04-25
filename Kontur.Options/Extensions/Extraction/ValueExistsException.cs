using System;

namespace Kontur.Options
{
    public sealed class ValueExistsException : InvalidOperationException
    {
        internal ValueExistsException(string message)
            : base(message)
        {
        }
    }
}
