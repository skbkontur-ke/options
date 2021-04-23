using System;

namespace Kontur.Options
{
    public static class OnNoneExtensions
    {
        public static Option<TValue> OnNone<TValue>(this IOption<TValue> option, Action action)
        {
            return option.Switch(action, () => { });
        }
    }
}