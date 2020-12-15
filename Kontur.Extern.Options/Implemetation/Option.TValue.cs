using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace Kontur.Extern.Options
{
    public abstract class Option<TValue>
        : IEnumerable<TValue>, IOptionMatch<TValue>
    {
        protected static readonly Type TypeArgument = typeof(TValue);

        private protected Option()
        {
        }

        public bool HasSome => Match(_ => true, false);

        public bool IsNone => !HasSome;

        public static implicit operator Option<TValue>(TValue value)
        {
            return new Some<TValue>(value);
        }

        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1313:Parameter names should begin with lower-case letter", Justification = "Only type is needed")]
        public static implicit operator Option<TValue>(NoneMarker _)
        {
            return new None<TValue>();
        }

        public static implicit operator bool(Option<TValue> option)
        {
            return option.HasSome;
        }

        [Pure]
        public static Option<TValue> Some(TValue value)
        {
            return new Some<TValue>(value);
        }

        [Pure]
        public static Option<TValue> None()
        {
            return new None<TValue>();
        }

        IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator() => GetEnumeratorInternal();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumeratorInternal();

        TResult IOptionMatch<TValue>.Match<TResult>(Func<TValue, TResult> onSome, Func<TResult> onNone) =>
            Match(onSome, onNone);

#if !NETSTANDARD2_0
        [Pure]
        public abstract bool TryGet([MaybeNullWhen(returnValue: false)] out TValue value);
#endif

        public abstract TResult Match<TResult>(Func<TValue, TResult> onSome, Func<TResult> onNone);

        public TResult Match<TResult>(Func<TValue, TResult> onSome, TResult onNoneValue)
        {
            return Match(onSome, () => onNoneValue);
        }

        public Option<TResult> Map<TResult>(Func<TValue, TResult> mapper)
        {
            return Select(value => Option<TResult>.Some(mapper(value)));
        }

        public Option<TResult> Select<TResult>(Func<TValue, Option<TResult>> resultSelector)
        {
            return Match(resultSelector, Option.None<TResult>);
        }

        public Option<TResult> Select<TResult>(Func<TValue, TResult> resultSelector)
        {
            return Select(value => Option.Some(resultSelector(value)));
        }

        public Option<TValue> Switch(Action<TValue> onSome, Action onNone)
        {
            SwitchInternal(onSome, onNone);
            return this;
        }

        private protected abstract void SwitchInternal(Action<TValue> onSome, Action onNone);

        private protected abstract IEnumerator<TValue> GetEnumeratorInternal();
    }
}