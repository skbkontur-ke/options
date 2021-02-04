using System;

namespace Kontur.Options
{
    public static class SwitchExtensions
    {
        public static Option<TValue> Switch<TValue>(this Option<TValue> option, Action onNone, Action onSome)
        {
            return option.Switch(onNone, _ => onSome());
        }
    }
}