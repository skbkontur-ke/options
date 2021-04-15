using System;

namespace Kontur.Options
{
    internal sealed class None<TValue> : Option<TValue>
    {
        public override TResult Match<TResult>(Func<TResult> onNone, Func<TValue, TResult> onSome)
        {
            return onNone();
        }

        public override string ToString()
        {
            return $"{nameof(None<TValue>)}{TypeArgumentString}";
        }
    }
}