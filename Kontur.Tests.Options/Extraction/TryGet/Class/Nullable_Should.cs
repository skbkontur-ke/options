using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Extraction.TryGet.Class
{
    [TestFixture]
    internal class Nullable_Should
    {
        private static TestCaseData CreateReturnBooleanCase(Option<string?> option, bool success)
        {
            return Common.CreateReturnBooleanCase(option, success);
        }

        private static readonly TestCaseData[] ReturnBooleanCases =
        {
            CreateReturnBooleanCase(Option.None(), false),
            CreateReturnBooleanCase(Option<string?>.Some(null), true),
            CreateReturnBooleanCase("foo", true),
        };

        [TestCaseSource(nameof(ReturnBooleanCases))]
        public bool Return_Boolean(Option<string?> option)
        {
#if NETFRAMEWORK
// The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'class' constraint.
// Argument cannot be used for parameter due to differences in the nullability of reference types.
#pragma warning disable CS8634, CS8620
#endif
            return option.TryGet(out _);
#pragma warning restore CS8620, CS8634
        }

        private static TestCaseData CreateGetCase(string? expectedValue)
        {
            return new TestCaseData(Option.Some(expectedValue)).Returns(expectedValue);
        }

        private static readonly TestCaseData[] GetCases =
        {
            CreateGetCase(null),
            CreateGetCase("foo"),
        };

        [TestCaseSource(nameof(GetCases))]
        public string? Extract_Value(Option<string?> option)
        {
#if NETFRAMEWORK
#pragma warning disable CS8634 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'class' constraint.
#endif
            _ = option.TryGet(out var actual);
#pragma warning restore CS8634

            return actual;
        }

        [TestCaseSource(nameof(GetCases))]
        public string? Extract_Value_With_If(Option<string?> option)
        {
#if NETFRAMEWORK
#pragma warning disable CS8634 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'class' constraint.
#endif
            if (option.TryGet(out var result))
#pragma warning restore CS8634
            {
                return result;
            }

            throw new UnreachableException();
        }
    }
}
