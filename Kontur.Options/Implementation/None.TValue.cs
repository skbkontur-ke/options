using System;
using System.Diagnostics.Contracts;

namespace Kontur.Options
{
    internal sealed class None<TValue> : Option<TValue>
    {
        public override string ToString()
        {
            return $"{nameof(None<TValue>)}{TypeArgumentString}";
        }

        public override bool Equals(object obj)
        {
            return obj is None<TValue>;
        }

        public override int GetHashCode()
        {
            return TypeArgument.GetHashCode();
        }

        public override TResult Match<TResult>(Func<TResult> onNone, Func<TValue, TResult> onSome)
        {
            return onNone();
        }

        [Pure]
        public override bool TryGet(
#if NETSTANDARD2_0
            out TValue? value)
#else
            [System.Diagnostics.CodeAnalysis.MaybeNullWhen(returnValue: false)]
            out TValue value)
#endif
        {
            value = default;
            return false;
        }

        private protected override void SwitchInternal(Action onNone, Action<TValue> onSome)
        {
            onNone();
        }
    }
}