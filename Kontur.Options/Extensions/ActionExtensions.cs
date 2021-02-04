﻿using System;

namespace Kontur.Options
{
    public static class ActionExtensions
    {
        public static Option<TValue> OnSome<TValue>(this Option<TValue> option, Action<TValue> action)
        {
            return option.Switch(() => { }, action);
        }

        public static Option<TValue> OnSome<TValue>(this Option<TValue> option, Action action)
        {
            return option.OnSome(_ => action());
        }

        public static Option<TValue> OnNone<TValue>(this Option<TValue> option, Action action)
        {
            return option.Switch(action, () => { });
        }
    }
}