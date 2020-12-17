using System;

namespace Kontur.Options.Unsafe
{
    public class ValueMissingException : InvalidOperationException
    {
        public ValueMissingException(string message)
            : base(message)
        {
        }
    }
}
