using System.Collections.Generic;
using Kontur.Options;

namespace Kontur.Tests.Options
{
    internal static class UpcastExamples
    {
        internal static IEnumerable<(Option<Child> Source, Option<Base> Result)> Get()
        {
            var child = new Child();
            yield return (Option<Child>.Some(child), Option<Base>.Some(child));
            yield return (Option<Child>.None(), Option<Base>.None());
        }
    }
}
