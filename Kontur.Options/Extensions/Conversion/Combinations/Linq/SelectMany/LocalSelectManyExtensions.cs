﻿using System;
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
                async value => Option<TItemValue>.Some(await taskSelector(value).ConfigureAwait(false)),
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
                async item => Option<TValue2>.Some(await taskSelector(item).ConfigureAwait(false)),
                resultSelector);
        }

        public static Task<Option<TResult>> SelectMany<TValue1, TValue2, TResult>(
            this Task<TValue1> task,
            Func<TValue1, Option<TValue2>> optionSelector,
            Func<TValue1, TValue2, TResult> resultSelector)
        {
            return task.SelectMany(optionSelector, FunctionResultToOption.Wrap(resultSelector));
        }

        public static async Task<Option<TResult>> SelectMany<TValue1, TValue2, TResult>(
            this Task<TValue1> task,
            Func<TValue1, Option<TValue2>> optionSelector,
            Func<TValue1, TValue2, Option<TResult>> resultSelector)
        {
            var value1 = await task.ConfigureAwait(false);
            return optionSelector(value1).Select(value2 => resultSelector(value1, value2));
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
            return option.Select(value2 => resultSelector(value1, value2));
        }

        public static Task<Option<TResult>> SelectMany<TValue, TItemValue, TResult>(
            this Option<TValue> option,
            Func<TValue, Task<TItemValue>> taskSelector,
            Func<TValue, TItemValue, Task<TResult>> resultSelector)
        {
            return option.SelectMany(taskSelector, FunctionResultToOption.Wrap(resultSelector));
        }

        public static Task<Option<TResult>> SelectMany<TValue, TItemValue, TResult>(
            this Option<TValue> option,
            Func<TValue, Task<TItemValue>> taskSelector,
            Func<TValue, TItemValue, Task<Option<TResult>>> resultSelector)
        {
            return option.SelectMany(
                async value => Option<TItemValue>.Some(await taskSelector(value).ConfigureAwait(false)),
                resultSelector);
        }

        public static Task<Option<TResult>> SelectMany<TValue1, TValue2, TResult>(
            this Task<Option<TValue1>> optionTask,
            Func<TValue1, Task<TValue2>> taskSelector,
            Func<TValue1, TValue2, Task<TResult>> resultSelector)
        {
            return optionTask.SelectMany(taskSelector, FunctionResultToOption.Wrap(resultSelector));
        }

        public static Task<Option<TResult>> SelectMany<TValue1, TValue2, TResult>(
            this Task<Option<TValue1>> optionTask,
            Func<TValue1, Task<TValue2>> taskSelector,
            Func<TValue1, TValue2, Task<Option<TResult>>> resultSelector)
        {
            return optionTask.SelectMany(
                async item => Option<TValue2>.Some(await taskSelector(item).ConfigureAwait(false)),
                resultSelector);
        }

        public static Task<Option<TResult>> SelectMany<TValue1, TValue2, TResult>(
            this Task<TValue1> task,
            Func<TValue1, Option<TValue2>> optionSelector,
            Func<TValue1, TValue2, Task<TResult>> resultSelector)
        {
            return task.SelectMany(optionSelector, FunctionResultToOption.Wrap(resultSelector));
        }

        public static async Task<Option<TResult>> SelectMany<TValue1, TValue2, TResult>(
            this Task<TValue1> task,
            Func<TValue1, Option<TValue2>> optionSelector,
            Func<TValue1, TValue2, Task<Option<TResult>>> resultSelector)
        {
            var value1 = await task.ConfigureAwait(false);
            return await optionSelector(value1).Select(value2 => resultSelector(value1, value2)).ConfigureAwait(false);
        }

        public static Task<Option<TResult>> SelectMany<TValue1, TValue2, TResult>(
            this Task<TValue1> task,
            Func<TValue1, Task<Option<TValue2>>> optionSelector,
            Func<TValue1, TValue2, Task<TResult>> resultSelector)
        {
            return task.SelectMany(optionSelector, FunctionResultToOption.Wrap(resultSelector));
        }

        public static async Task<Option<TResult>> SelectMany<TValue1, TValue2, TResult>(
            this Task<TValue1> task,
            Func<TValue1, Task<Option<TValue2>>> optionSelector,
            Func<TValue1, TValue2, Task<Option<TResult>>> resultSelector)
        {
            var value1 = await task.ConfigureAwait(false);
            var option = await optionSelector(value1).ConfigureAwait(false);
            return await option.Select(value2 => resultSelector(value1, value2)).ConfigureAwait(false);
        }
    }
}