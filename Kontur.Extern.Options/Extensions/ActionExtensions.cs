using System;

namespace Kontur.Extern.Options
{
    public static class ActionExtensions
    {
        public static Option<TValue> OnSome<TValue>(this Option<TValue> option, Action<TValue> action)
        {
            return option.Switch(action, () => { });
        }

        public static Option<TValue> OnSome<TValue>(this Option<TValue> option, Action action)
        {
            return option.Switch(_ => action(), () => { });
        }

        public static Option<TValue> OnNone<TValue>(this Option<TValue> option, Action action)
        {
            return option.Switch(_ => { }, action);
        }
    }
}