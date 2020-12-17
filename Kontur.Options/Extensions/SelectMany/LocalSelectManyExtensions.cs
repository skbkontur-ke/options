using System;
using System.Threading.Tasks;

namespace Kontur.Options
{
    public static class LocalSelectManyExtensions
    {
        public static Task<Option<TResult>> SelectMany<TValue, TItemValue, TResult>(
            this Option<TValue> option,
            Func<TValue, Task<TItemValue>> taskSelector,
            Func<TValue, TItemValue, TResult> resultSelector)
        {
            return option.SelectMany(taskSelector, FunctionResultToOption.Wrap(resultSelector));
        }

        public static Task<Option<TResult>> SelectMany<TValue, TItemValue, TResult>(
            this Option<TValue> option,
            Func<TValue, Task<TItemValue>> taskSelector,
            Func<TValue, TItemValue, Option<TResult>> resultSelector)
        {
            return option.SelectMany(
                async value => Option.Some(await taskSelector(value).ConfigureAwait(false)),
                resultSelector);
        }

        public static Task<Option<TResult>> SelectMany<TValue1, TValue2, TResult>(
            this Task<Option<TValue1>> optionTask,
            Func<TValue1, Task<TValue2>> taskSelector,
            Func<TValue1, TValue2, TResult> resultSelector)
        {
            return optionTask.SelectMany(taskSelector, FunctionResultToOption.Wrap(resultSelector));
        }

        public static Task<Option<TResult>> SelectMany<TValue1, TValue2, TResult>(
            this Task<Option<TValue1>> optionTask,
            Func<TValue1, Task<TValue2>> taskSelector,
            Func<TValue1, TValue2, Option<TResult>> resultSelector)
        {
            return optionTask.SelectMany(
                async item => Option.Some(await taskSelector(item).ConfigureAwait(false)),
                resultSelector);
        }

        public static Task<Option<TResult>> SelectMany<TValue1, TValue2, TResult>(
            this Task<TValue1> task,
            Func<TValue1, Option<TValue2>> optionSelector,
            Func<TValue1, TValue2, TResult> resultSelector)
        {
            return task.SelectMany(optionSelector, FunctionResultToOption.Wrap(resultSelector));
        }

        public static Task<Option<TResult>> SelectMany<TValue1, TValue2, TResult>(
            this Task<TValue1> task,
            Func<TValue1, Option<TValue2>> optionSelector,
            Func<TValue1, TValue2, Option<TResult>> resultSelector)
        {
            return task.SelectMany(
                item => Task.FromResult(optionSelector(item)),
                resultSelector);
        }

        public static Task<Option<TResult>> SelectMany<TValue1, TValue2, TResult>(
            this Task<TValue1> task,
            Func<TValue1, Task<Option<TValue2>>> optionSelector,
            Func<TValue1, TValue2, TResult> resultSelector)
        {
            return task.SelectMany(optionSelector, FunctionResultToOption.Wrap(resultSelector));
        }

        public static async Task<Option<TResult>> SelectMany<TValue1, TValue2, TResult>(
            this Task<TValue1> task,
            Func<TValue1, Task<Option<TValue2>>> optionSelector,
            Func<TValue1, TValue2, Option<TResult>> resultSelector)
        {
            var value1 = await task.ConfigureAwait(false);
            var option = await optionSelector(value1).ConfigureAwait(false);
            return option.Match(value2 => resultSelector(value1, value2), Option.None<TResult>);
        }
    }
}