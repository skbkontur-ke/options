using System;
using System.Threading.Tasks;
using Kontur.Extern.Options;

// ReSharper disable CheckNamespace
#pragma warning disable S3903 // Types should be defined in named namespaces
public static class GlobalSelectManyExtensions
#pragma warning restore S3903 // Types should be defined in named namespaces

// ReSharper restore CheckNamespace
{
    public static Option<TResult> SelectMany<TValue, TOtherValue, TResult>(
        this Option<TValue> option,
        Func<TValue, Option<TOtherValue>> optionSelector,
        Func<TValue, TOtherValue, TResult> resultSelector)
    {
        return option.SelectMany(optionSelector, FunctionResultToOption.Wrap(resultSelector));
    }

    public static Option<TResult> SelectMany<TValue, TOtherValue, TResult>(
        this Option<TValue> option,
        Func<TValue, Option<TOtherValue>> optionSelector,
        Func<TValue, TOtherValue, Option<TResult>> resultSelector)
    {
        return option.Match(
            myValue => optionSelector(myValue).Match(
                otherValue => resultSelector(myValue, otherValue),
                Option.None<TResult>),
            Option.None<TResult>);
    }

    public static Task<Option<TResult>> SelectMany<TValue, TItemValue, TResult>(
        this Option<TValue> option,
        Func<TValue, Task<Option<TItemValue>>> optionSelector,
        Func<TValue, TItemValue, TResult> resultSelector)
    {
        return option.SelectMany(optionSelector, FunctionResultToOption.Wrap(resultSelector));
    }

    public static Task<Option<TResult>> SelectMany<TValue, TItemValue, TResult>(
        this Option<TValue> option,
        Func<TValue, Task<Option<TItemValue>>> optionSelector,
        Func<TValue, TItemValue, Option<TResult>> resultSelector)
    {
        return option.Match(
            async value =>
            {
                var item = await optionSelector(value).ConfigureAwait(false);
                return item.Match(
                    itemValue => resultSelector(value, itemValue),
                    Option<TResult>.None);
            },
            () => Task.FromResult(Option.None<TResult>()));
    }

    public static Task<Option<TResult>> SelectMany<TValue1, TValue2, TResult>(
        this Task<Option<TValue1>> optionTask,
        Func<TValue1, Option<TValue2>> optionSelector,
        Func<TValue1, TValue2, TResult> resultSelector)
    {
        return optionTask.SelectMany(optionSelector, FunctionResultToOption.Wrap(resultSelector));
    }

    public static Task<Option<TResult>> SelectMany<TValue1, TValue2, TResult>(
        this Task<Option<TValue1>> optionTask,
        Func<TValue1, Option<TValue2>> optionSelector,
        Func<TValue1, TValue2, Option<TResult>> resultSelector)
    {
        return optionTask.SelectMany(
            value => Task.FromResult(optionSelector(value)),
            resultSelector);
    }

    public static Task<Option<TResult>> SelectMany<TValue1, TValue2, TResult>(
        this Task<Option<TValue1>> optionTask,
        Func<TValue1, Task<Option<TValue2>>> optionSelector,
        Func<TValue1, TValue2, TResult> resultSelector)
    {
        return optionTask.SelectMany(optionSelector, FunctionResultToOption.Wrap(resultSelector));
    }

    public static async Task<Option<TResult>> SelectMany<TValue1, TValue2, TResult>(
        this Task<Option<TValue1>> optionTask,
        Func<TValue1, Task<Option<TValue2>>> optionSelector,
        Func<TValue1, TValue2, Option<TResult>> resultSelector)
    {
        var option = await optionTask.ConfigureAwait(false);
        return await option.SelectMany(optionSelector, resultSelector).ConfigureAwait(false);
    }
}