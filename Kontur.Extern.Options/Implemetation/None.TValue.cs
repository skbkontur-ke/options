using System;
using System.Collections.Generic;

namespace Kontur.Extern.Options
{
    internal sealed class None<TValue> : Option<TValue>
    {
        public override string ToString()
        {
            return $"{nameof(None<TValue>)}<{TypeArgument.Name}>";
        }

        public override bool Equals(object obj)
        {
            return obj is None<TValue>;
        }

        public override int GetHashCode()
        {
            return TypeArgument.GetHashCode();
        }

        public override TResult Match<TResult>(Func<TValue, TResult> onSome, Func<TResult> onNone)
        {
            return onNone();
        }

#if !NETSTANDARD2_0
        [System.Diagnostics.Contracts.Pure]
        public override bool TryGet(
            [System.Diagnostics.CodeAnalysis.MaybeNullWhen(returnValue: false)]
            out TValue value)
        {
            value = default;
            return false;
        }
#endif

        private protected override void SwitchInternal(Action<TValue> onSome, Action onNone)
        {
            onNone();
        }

        private protected override IEnumerator<TValue> GetEnumeratorInternal()
        {
            yield break;
        }
    }
}