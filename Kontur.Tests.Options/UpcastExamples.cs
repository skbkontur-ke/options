using System;
using System.Collections.Generic;
using Kontur.Options;

namespace Kontur.Tests.Options
{
    internal static class UpcastExamples
    {
        internal static IEnumerable<UpcastExample<Child, Option<Base>>> Get()
            => Get(Option<Base>.None(), Option<Base>.Some);

        internal static IEnumerable<UpcastExample<Child, TResult>> Get<TResult>(TResult noneResult, Func<Child, TResult> someResultFactory)
        {
            var child = new Child();
            yield return new(Option<Child>.Some(child), someResultFactory(child));
            yield return new(Option<Child>.None(), noneResult);
        }
    }
}
