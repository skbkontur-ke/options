using System;
using System.Threading.Tasks;
using Kontur.Options;

// ReSharper disable once CheckNamespace
public static class GlobalSelectManyExtensions
{
    public static Task<Option<TResult>> SelectMany<TValue1, TValue2, TResult>(
        this Task<Option<TValue1>> optionTask,
        Func<TValue1, Option<TValue2>> optionSelector,
        Func<TValue1, TValue2, TResult> resultSelector)
    {
        return optionTask.SelectMany(optionSelector, FunctionResultToOption.Wrap(resultSelector));
    }

    public static async Task<Option<TResult>> SelectMany<TValue1, TValue2, TResult>(
        this Task<Option<TValue1>> optionTask,
        Func<TValue1, Option<TValue2>> optionSelector,
        Func<TValue1, TValue2, Option<TResult>> resultSelector)
    {
        var option = await optionTask.ConfigureAwait(false);
        return option.SelectMany(optionSelector, resultSelector);
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

    public static Task<Option<TResult>> SelectMany<TValue1, TValue2, TResult>(
        this Task<Option<TValue1>> optionTask,
        Func<TValue1, Option<TValue2>> optionSelector,
        Func<TValue1, TValue2, Task<TResult>> resultSelector)
    {
        return optionTask.SelectMany(optionSelector, FunctionResultToOption.Wrap(resultSelector));
    }

    public static async Task<Option<TResult>> SelectMany<TValue1, TValue2, TResult>(
        this Task<Option<TValue1>> optionTask,
        Func<TValue1, Option<TValue2>> optionSelector,
        Func<TValue1, TValue2, Task<Option<TResult>>> resultSelector)
    {
        var option = await optionTask.ConfigureAwait(false);
        return await option.SelectMany(optionSelector, resultSelector).ConfigureAwait(false);
    }

    public static Task<Option<TResult>> SelectMany<TValue1, TValue2, TResult>(
        this Task<Option<TValue1>> optionTask,
        Func<TValue1, Task<Option<TValue2>>> optionSelector,
        Func<TValue1, TValue2, Task<TResult>> resultSelector)
    {
        return optionTask.SelectMany(optionSelector, FunctionResultToOption.Wrap(resultSelector));
    }

    public static async Task<Option<TResult>> SelectMany<TValue1, TValue2, TResult>(
        this Task<Option<TValue1>> optionTask,
        Func<TValue1, Task<Option<TValue2>>> optionSelector,
        Func<TValue1, TValue2, Task<Option<TResult>>> resultSelector)
    {
        var option = await optionTask.ConfigureAwait(false);
        return await option.SelectMany(optionSelector, resultSelector).ConfigureAwait(false);
    }
}
