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
            yield return CreateEqual(Option<string>.None());
            yield return CreateEqual(Option<int>.None());
            yield return CreateEqual(Option<int>.Some(2));
            yield return CreateEqual(Option<string?>.Some(null));
            yield return CreateEqual(Option<string?>.Some("hello"));
            yield return CreateEqual(Option<string>.Some("hello"));
        }

        private static IEnumerable<TestCaseData> CreateNotEqual<TValue1, TValue2>(Option<TValue1> option1, Option<TValue2> option2)
        {
            yield return new(option1, option2);
            yield return new(option2, option1);
        }

        private static IEnumerable<IEnumerable<TestCaseData>> CreateNonEqualsCaseTemplates()
        {
            yield return CreateNotEqual(Option<string>.None(), Option<int>.None());

            yield return CreateNotEqual(Option<int>.None(), Option<int>.Some(1));

            yield return CreateNotEqual(Option<int>.Some(1), Option<int>.Some(2));
            yield return CreateNotEqual(Option<string>.Some("hello"), Option<string>.Some("hi"));
            yield return CreateNotEqual(Option<int>.Some(2), Option<double>.Some(2.0));
        }

        internal static IEnumerable<TestCaseData> CreateNonEqualsCases()
        {
            return CreateNonEqualsCaseTemplates().SelectMany(c => c);
        }
    }
}
