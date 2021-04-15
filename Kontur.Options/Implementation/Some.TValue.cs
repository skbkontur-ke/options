using System;

namespace Kontur.Options
{
    internal sealed class Some<TValue> : Option<TValue>
    {
        private readonly TValue value;

        internal Some(TValue value)
        {
            this.value = value;
        }

        public override TResult Match<TResult>(Func<TResult> onNone, Func<TValue, TResult> onSome)
        {
            return onSome(value);
        }
    }
}