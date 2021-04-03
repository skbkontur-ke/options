using System.Collections.Generic;
using System.Linq;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Equality
{
    internal static class Common
    {
        private static TestCaseData Create<TValue1, TValue2>(Option<TValue1> option1, Option<TValue2> option2)
        {
            return new(option1, option2);
        }

        private static TestCaseData CreateEqual<TValue>(Option<TValue> option)
        {
            return Create(option, option);
        }

        internal static IEnumerable<TestCaseData> CreateEqualsCases()
        {
            yield return CreateEqual(Option.None<string>());
            yield return CreateEqual(Option.None<int>());
            yield return CreateEqual(Option.Some(2));
            yield return CreateEqual(Option.Some<string?>(null));
            yield return CreateEqual(Option.Some("hello"));
        }

        private static IEnumerable<TestCaseData> CreateNotEqual<TValue1, TValue2>(Option<TValue1> option1, Option<TValue2> option2)
        {
            yield return new(option1, option2);
            yield return new(option2, option1);
        }

        private static IEnumerable<IEnumerable<TestCaseData>> CreateNonEqualsCaseTemplates()
        {
            yield return CreateNotEqual(Option.None<string>(), Option.None<int>());

            yield return CreateNotEqual(Option.None<int>(), Option.Some(1));

            yield return CreateNotEqual(Option.Some(1), Option.Some(2));
            yield return CreateNotEqual(Option.Some("hello"), Option.Some("hi"));
            yield return CreateNotEqual(Option.Some(2), Option.Some(2.0));
        }

        internal static IEnumerable<TestCaseData> CreateNonEqualsCases()
        {
            return CreateNonEqualsCaseTemplates().SelectMany(c => c);
        }
    }
}
