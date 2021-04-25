using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using Kontur.Options.Containers;

namespace Kontur.Options
{
    public abstract class Option<TValue> : IOption<TValue>
    {
        private static readonly Type TypeArgument = typeof(TValue);

        private protected Option()
        {
        }

        public bool HasSome => Match(false, true);

        public bool IsNone => !HasSome;

        public static implicit operator bool(Option<TValue> option)
        {
            return option.HasSome;
        }

        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1313:Parameter names should begin with lower-case letter", Justification = "Only type is needed")]
        public static implicit operator Option<TValue>(NoneMarker _)
        {
            return None();
        }

        public static implicit operator Option<TValue>(TValue value)
        {
            return Some(value);
        }

        [Pure]
        public static Option<TValue> None()
        {
            return None<TValue>.Instance;
        }

        [Pure]
        public static Option<TValue> Some(TValue value)
        {
            return new Some<TValue>(value);
        }

        TResult IOption<TValue>.Match<TResult>(Func<TResult> onNone, Func<TValue, TResult> onSome) =>
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

        public Task<Option<TResult>> Select<TResult>(Func<TValue, Task<Option<TResult>>> resultSelector)
        {
            return Match(() => Task.FromResult(Option<TResult>.None()), resultSelector);
        }

        public Task<Option<TResult>> Select<TResult>(Func<TValue, Task<TResult>> resultSelector)
        {
            return Select(async value => Option<TResult>.Some(await resultSelector(value).ConfigureAwait(false)));
        }

        public Option<TValue> Where(Func<TValue, bool> predicate)
        {
            return Select(value => predicate(value) ? Some(value) : None());
        }

        public Task<Option<TValue>> Where(Func<TValue, Task<bool>> predicate)
        {
            return Select(async value => await predicate(value).ConfigureAwait(false)
                ? Some(value)
                : None());
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

        public Task<Option<TResult>> SelectMany<TOtherValue, TResult>(
            Func<TValue, Option<TOtherValue>> optionSelector,
            Func<TValue, TOtherValue, Task<TResult>> resultSelector)
        {
            return SelectMany(optionSelector, FunctionResultToOption.Wrap(resultSelector));
        }

        public Task<Option<TResult>> SelectMany<TOtherValue, TResult>(
            Func<TValue, Option<TOtherValue>> optionSelector,
            Func<TValue, TOtherValue, Task<Option<TResult>>> resultSelector)
        {
            return Select(
                value => optionSelector(value).Select(
                    otherValue => resultSelector(value, otherValue)));
        }

        public Task<Option<TResult>> SelectMany<TOtherValue, TResult>(
            Func<TValue, Task<Option<TOtherValue>>> optionSelector,
            Func<TValue, TOtherValue, Task<TResult>> resultSelector)
        {
            return SelectMany(optionSelector, FunctionResultToOption.Wrap(resultSelector));
        }

        public Task<Option<TResult>> SelectMany<TOtherValue, TResult>(
            Func<TValue, Task<Option<TOtherValue>>> optionSelector,
            Func<TValue, TOtherValue, Task<Option<TResult>>> resultSelector)
        {
            return Match(
                () => Task.FromResult(Option<TResult>.None()),
                async value =>
                {
                    var otherValue = await optionSelector(value).ConfigureAwait(false);
                    return await otherValue.Select(itemValue => resultSelector(value, itemValue)).ConfigureAwait(false);
                });
        }

        [Pure]
        public bool TryGet([MaybeNullWhen(returnValue: false)] out TValue value)
        {
            return Match(
                    () => NoneContainer<TValue>.Instance,
                    val => new SomeContainer<TValue>(val))
                .TryGet(out value);
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

        public sealed override string ToString()
        {
            var typeArguments = $"<{TypeArgument.Name}>";
            return Match(
                () => $"{nameof(None<TValue>)}{typeArguments}",
                value => $"{nameof(Some<TValue>)}{typeArguments} value={value}");
        }

        public sealed override bool Equals(object obj)
        {
            return obj is Option<TValue> other && other.GetState().Equals(GetState());
        }

        public sealed override int GetHashCode()
        {
            return (TypeArgument, GetState()).GetHashCode();
        }

        [Pure]
        private (bool Success, TValue? Value) GetState()
        {
            return Match<(bool, TValue?)>(() => (false, default), value => (true, value));
        }
    }
}