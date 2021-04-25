using System;
using System.Threading.Tasks;

namespace Kontur.Options
{
    public static class LinqSelectExtensions
    {
        public static Option<TResult> Select<TValue, TResult>(
            this Option<TValue> option,
            Func<TValue, TResult> resultSelector)
        {
            return option.Select(value => Option<TResult>.Some(resultSelector(value)));
        }

        public static Task<Option<TResult>> Select<TValue, TResult>(
            this Option<TValue> option,
            Func<TValue, Task<Option<TResult>>> resultSelector)
        {
            return option.Match(() => Task.FromResult(Option<TResult>.None()), resultSelector);
        }

        public static Task<Option<TResult>> Select<TValue, TResult>(
            this Option<TValue> option,
            Func<TValue, Task<TResult>> resultSelector)
        {
            return option.Select(async value => Option<TResult>.Some(await resultSelector(value).ConfigureAwait(false)));
        }

        public static async Task<Option<TResult>> Select<TValue, TResult>(
            this Task<Option<TValue>> optionTask,
            Func<TValue, TResult> resultSelector)
        {
            var option = await optionTask.ConfigureAwait(false);
            return option.Select(resultSelector);
        }

        public static async Task<Option<TResult>> Select<TValue, TResult>(
            this Task<Option<TValue>> optionTask,
            Func<TValue, Option<TResult>> resultSelector)
        {
            var option = await optionTask.ConfigureAwait(false);
            return option.Select(resultSelector);
        }

        public static async Task<Option<TResult>> Select<TValue, TResult>(
            this Task<Option<TValue>> optionTask,
            Func<TValue, Task<TResult>> resultSelector)
        {
            var option = await optionTask.ConfigureAwait(false);
            return await option.Select(resultSelector).ConfigureAwait(false);
        }

        public static async Task<Option<TResult>> Select<TValue, TResult>(
            this Task<Option<TValue>> optionTask,
            Func<TValue, Task<Option<TResult>>> resultSelector)
        {
            var option = await optionTask.ConfigureAwait(false);
            return await option.Select(resultSelector).ConfigureAwait(false);
        }
    }
}