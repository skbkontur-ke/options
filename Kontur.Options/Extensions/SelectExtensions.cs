using System;
using System.Threading.Tasks;

namespace Kontur.Options
{
    public static class SelectExtensions
    {
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