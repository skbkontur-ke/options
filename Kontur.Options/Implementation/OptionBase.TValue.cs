using System;
using System.Diagnostics.Contracts;

namespace Kontur.Options
{
    public abstract class OptionBase<TValue> : IOptionMatchable<TValue>
    {
        public bool HasSome => Match(false, true);

        public bool IsNone => !HasSome;

        public static implicit operator bool(OptionBase<TValue> option)
        {
            return option.HasSome;
        }

        TResult IOptionMatchable<TValue>.Match<TResult>(Func<TResult> onNone, Func<TValue, TResult> onSome) =>
            Match(onNone, onSome);

        [Pure]
        public TResult Match<TResult>(TResult onNoneValue, TResult onSomeValue)
        {
            return Match(() => onNoneValue, onSomeValue);
        }

        public TResult Match<TResult>(TResult onNoneValue, Func<TResult> onSome)
        {
            return Match(() => onNoneValue, onSome);
        }

        public TResult Match<TResult>(TResult onNoneValue, Func<TValue, TResult> onSome)
        {
            return Match(() => onNoneValue, onSome);
        }

        public TResult Match<TResult>(Func<TResult> onNone, TResult onSomeValue)
        {
            return Match(onNone, () => onSomeValue);
        }

        public TResult Match<TResult>(Func<TResult> onNone, Func<TResult> onSome)
        {
            return Match(onNone, _ => onSome());
        }

        public abstract TResult Match<TResult>(Func<TResult> onNone, Func<TValue, TResult> onSome);

        [Pure]
        public abstract bool TryGet(
#if NETSTANDARD2_0
            out TValue? value);
#else
            [System.Diagnostics.CodeAnalysis.MaybeNullWhen(returnValue: false)] out TValue value);
#endif
    }
}