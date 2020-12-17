using System;
using System.Threading.Tasks;
using Kontur.Options;

// ReSharper disable once CheckNamespace
#pragma warning disable S3903 // Types should be defined in named namespaces
public static class GlobalSelectManyExtensions
#pragma warning restore S3903 // Types should be defined in named namespaces
{
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
