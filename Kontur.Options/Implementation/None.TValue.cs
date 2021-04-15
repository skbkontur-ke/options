using System;

namespace Kontur.Options
{
    internal sealed class None<TValue> : Option<TValue>
    {
        public override TResult Match<TResult>(Func<TResult> onNone, Func<TValue, TResult> onSome)
        {
            return onNone();
        }
    }
}