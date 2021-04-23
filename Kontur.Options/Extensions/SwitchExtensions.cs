using System;

namespace Kontur.Options
{
    public static class SwitchExtensions
    {
        public static Option<TValue> Switch<TValue>(this IOption<TValue> option, Action onNone, Action onSome)
        {
            return option.Switch(onNone, _ => onSome());
        }

        public static Option<TValue> Switch<TValue>(this IOption<TValue> option, Action onNone, Action<TValue> onSome)
        {
            return option.Match(
                () =>
                {
                    onNone();
                    return option;
                },
                value =>
                {
                    onSome(value);
                    return option;
                })
                .Upcast();
        }
    }
}