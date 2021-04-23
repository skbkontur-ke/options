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
            var converted = option.Upcast();
            return option.Match(
                () =>
                {
                    onNone();
                    return converted;
                },
                value =>
                {
                    onSome(value);
                    return converted;
                });
        }
    }
}