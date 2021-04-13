﻿using System;

namespace Kontur.Options
{
    public static class OnSomeExtensions
    {
        public static Option<TValue> OnSome<TValue>(this IOptionMatchable<TValue> option, Action<TValue> action)
        {
            return option.Switch(() => { }, action);
        }

        public static Option<TValue> OnSome<TValue>(this IOptionMatchable<TValue> option, Action action)
        {
            return option.OnSome(_ => action());
        }
    }
}