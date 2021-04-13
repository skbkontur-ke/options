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

        public override string ToString()
        {
            return $"{nameof(Some<TValue>)}{TypeArgumentString} value={value}";
        }

        public override bool Equals(object obj)
        {
            return obj is Some<TValue> other && Equals(value, other.value);
        }

        public override int GetHashCode()
        {
            return (value, TypeArgument).GetHashCode();
        }
    }
}