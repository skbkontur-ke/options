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
            return (Value: value, TypeArgument).GetHashCode();
        }

        public override TResult Match<TResult>(Func<TResult> onNone, Func<TValue, TResult> onSome)
        {
            return onSome(value);
        }

#if !NETSTANDARD2_0
        // ReSharper disable once ParameterHidesMember
        [System.Diagnostics.Contracts.Pure]
        public override bool TryGet(out TValue value)
        {
            value = this.value;
            return true;
        }
#endif

        private protected override void SwitchInternal(Action onNone, Action<TValue> onSome)
        {
            onSome(value);
        }
    }
}