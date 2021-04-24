using System;
using System.Threading.Tasks;

namespace Kontur.Options
{
    public static class WhereExtensions
    {
        public static async Task<Option<TValue>> Where<TValue>(
            this Task<Option<TValue>> optionTask,
            Func<TValue, bool> predicate)
        {
            var option = await optionTask.ConfigureAwait(false);
            return option.Where(predicate);
        }

        public static async Task<Option<TValue>> Where<TValue>(
            this Task<Option<TValue>> optionTask,
            Func<TValue, Task<bool>> predicate)
        {
            var option = await optionTask.ConfigureAwait(false);
            return await option.Where(predicate).ConfigureAwait(false);
        }
    }
}