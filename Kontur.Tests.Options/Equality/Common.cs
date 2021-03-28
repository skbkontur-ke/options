using System.Collections.Generic;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Equality
{
    internal static class Common
    {
        internal static TestCaseData Create<TValue1, TValue2>(Option<TValue1> option1, Option<TValue2> option2)
        {
            return new(option1, option2);
        }

        internal static IEnumerable<TestCaseData> CreateEqualsCases()
        {
            yield return Create(Option.None<string>(), Option.None<string>());
            yield return Create(Option.None<int>(), Option.None<int>());
            yield return Create(Option.Some(2), Option.Some(2));
            yield return Create(Option.Some<string?>(null), Option.Some<string?>(null));
            yield return Create(Option.Some("hello"), Option.Some("hello"));
        }

        internal static IEnumerable<TestCaseData> CreateNonEqualsCases()
        {
            yield return Create(Option.None<string>(), Option.None<int>());
            yield return Create(Option.None<int>(), Option.None<string>());

            yield return Create(Option.None<int>(), Option.Some(1));
            yield return Create(Option.Some(1), Option.None<int>());
            yield return Create(Option.Some(1), Option.Some(2));
            yield return Create(Option.Some("hello"), Option.Some("hi"));

            yield return Create(Option.Some(2), Option.Some(2.0));
        }
    }
}
