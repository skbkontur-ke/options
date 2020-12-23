using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace Kontur.Options
{
    public abstract class Option<TValue>
        : IEnumerable<TValue>, IOptionMatch<TValue>
    {
        protected static readonly Type TypeArgument = typeof(TValue);

        private protected Option()
        {
        }

        public bool HasSome => Match(false, _ => true);

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

        TResult IOptionMatch<TValue>.Match<TResult>(Func<TResult> onNone, Func<TValue, TResult> onSome) =>
            Match(onNone, onSome);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<TValue> GetEnumerator()
        {
            return Match(Enumerable.Empty<TValue>, value => new[] { value }).GetEnumerator();
        }

        public Option<TResult> Map<TResult>(Func<TValue, TResult> mapper)
        {
            return Select(value => Option<TResult>.Some(mapper(value)));
        }

        public Option<TResult> Select<TResult>(Func<TValue, Option<TResult>> resultSelector)
        {
            return Match(Option.None<TResult>, resultSelector);
        }

        public Option<TResult> Select<TResult>(Func<TValue, TResult> resultSelector)
        {
            return Select(value => Option.Some(resultSelector(value)));
        }

        public Option<TResult> SelectMany<TOtherValue, TResult>(
            Func<TValue, Option<TOtherValue>> optionSelector,
            Func<TValue, TOtherValue, TResult> resultSelector)
        {
            return SelectMany(optionSelector, FunctionResultToOption.Wrap(resultSelector));
        }

        public Option<TResult> SelectMany<TOtherValue, TResult>(
            Func<TValue, Option<TOtherValue>> optionSelector,
            Func<TValue, TOtherValue, Option<TResult>> resultSelector)
        {
            return Match(
                Option.None<TResult>,
                myValue => optionSelector(myValue).Match(
                    Option.None<TResult>,
                    otherValue => resultSelector(myValue, otherValue)));
        }

        public Task<Option<TResult>> SelectMany<TItemValue, TResult>(
            Func<TValue, Task<Option<TItemValue>>> optionSelector,
            Func<TValue, TItemValue, TResult> resultSelector)
        {
            return SelectMany(optionSelector, FunctionResultToOption.Wrap(resultSelector));
        }

        public Task<Option<TResult>> SelectMany<TItemValue, TResult>(
            Func<TValue, Task<Option<TItemValue>>> optionSelector,
            Func<TValue, TItemValue, Option<TResult>> resultSelector)
        {
            return Match(
                () => Task.FromResult(Option.None<TResult>()),
                async value =>
                {
                    var item = await optionSelector(value).ConfigureAwait(false);
                    return item.Match(Option<TResult>.None, itemValue => resultSelector(value, itemValue));
                });
        }

        public Option<TValue> Switch(Action onNone, Action<TValue> onSome)
        {
            SwitchInternal(onNone, onSome);
            return this;
        }

        public TResult Match<TResult>(TResult onNoneValue, Func<TValue, TResult> onSome)
        {
            return Match(() => onNoneValue, onSome);
        }

        public abstract TResult Match<TResult>(Func<TResult> onNone, Func<TValue, TResult> onSome);

#if !NETSTANDARD2_0
        [Pure]
        public abstract bool TryGet([MaybeNullWhen(returnValue: false)] out TValue value);
#endif

        private protected abstract void SwitchInternal(Action onNone, Action<TValue> onSome);
    }
}