﻿using System;
using System.Diagnostics.Contracts;

namespace Kontur.Options
{
    public static class GetOrElseExtensions
    {
        public static TValue GetOrElse<TValue>(this IOption<TValue> option, Func<TValue> onNoneValueFactory)
        {
            return option.Match(onNoneValueFactory, value => value);
        }

        [Pure]
        public static TValue GetOrElse<TValue>(this IOption<TValue> option, TValue onNoneValue)
        {
            return option.GetOrElse(() => onNoneValue);
        }
    }
}