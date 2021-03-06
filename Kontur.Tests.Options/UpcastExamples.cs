﻿using System;
using System.Collections.Generic;
using Kontur.Options;

namespace Kontur.Tests.Options
{
    internal static class UpcastExamples
    {
        internal static IEnumerable<UpcastExample<Option<Base>>> Get()
            => Get(Option<Base>.None(), Option<Base>.Some);

        internal static IEnumerable<UpcastExample<TResult>> Get<TResult>(TResult noneResult, Func<Child, TResult> someResultFactory)
        {
            Child child = new();
            yield return new(Option<Child>.Some(child), someResultFactory(child));
            yield return new(Option<Child>.None(), noneResult);
        }
    }
}
