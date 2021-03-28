using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;

namespace Kontur.Options
{
    public abstract class Option<TValue> : IMatchable<TValue>
    {
        protected static readonly Type TypeArgument = typeof(TValue);

        private protected Option()
        {
        }

        public bool HasSome => Match(false, true);

        public bool IsNone => !HasSome;

        protected static string TypeArgumentString => $"<{TypeArgument.Name}>";

        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1313:Parameter names should begin with lower-case letter", Justification = "Only type is needed")]
        public static implicit operator Option<TValue>(NoneMarker _)
        {
            return None();
        }

        public static implicit operator Option<TValue>(TValue value)
        {
            return Some(value);
        }

        public static implicit operator bool(Option<TValue> option)
        {
            return option.HasSome;
        }

        [Pure]
        public static Option<TValue> None()
        {
            return new None<TValue>();
        }

        [Pure]
        public static Option<TValue> Some(TValue value)
        {
            return new Some<TValue>(value);
        }

        TResult IMatchable<TValue>.Match<TResult>(Func<TResult> onNone, Func<TValue, TResult> onSome) =>
            Match(onNone, onSome);

        public Option<TResult> Map<TResult>(TResult result)
        {
            return Map(() => result);
        }

        public Option<TResult> Map<TResult>(Func<TResult> resultFactory)
        {
            return Map(_ => resultFactory());
        }

        public Option<TResult> Map<TResult>(Func<TValue, TResult> projection)
        {
            return Select(projection);
        }

        public Option<TResult> Select<TResult>(Func<TValue, Option<TResult>> resultSelector)
        {
            return Match(Option<TResult>.None, resultSelector);
        }

        public Option<TResult> Select<TResult>(Func<TValue, TResult> resultSelector)
        {
            return Select(value => Option<TResult>.Some(resultSelector(value)));
        }

        public Option<TValue> Where(Func<TValue, bool> predicate)
        {
            return Select(value => predicate(value) ? Some(value) : None());
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
            return Select(
                value => optionSelector(value).Select(
                    otherValue => resultSelector(value, otherValue)));
        }

        public Task<Option<TResult>> SelectMany<TOtherValue, TResult>(
            Func<TValue, Task<Option<TOtherValue>>> optionSelector,
            Func<TValue, TOtherValue, TResult> resultSelector)
        {
            return SelectMany(optionSelector, FunctionResultToOption.Wrap(resultSelector));
        }

        public Task<Option<TResult>> SelectMany<TOtherValue, TResult>(
            Func<TValue, Task<Option<TOtherValue>>> optionSelector,
            Func<TValue, TOtherValue, Option<TResult>> resultSelector)
        {
            return Match(
                () => Task.FromResult(Option<TResult>.None()),
                async value =>
                {
                    var otherValue = await optionSelector(value).ConfigureAwait(false);
                    return otherValue.Select(itemValue => resultSelector(value, itemValue));
                });
        }

        public Option<TValue> Switch(Action onNone, Action<TValue> onSome)
        {
            SwitchInternal(onNone, onSome);
            return this;
        }

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
            [MaybeNullWhen(returnValue: false)] out TValue value);
#endif

        public abstract override string ToString();

        public abstract override bool Equals(object obj);

        public abstract override int GetHashCode();

        private protected abstract void SwitchInternal(Action onNone, Action<TValue> onSome);
    }
}