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
            return option.TryGet(out _);
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
            _ = option.TryGet(out var value);

            return value;
        }

        [TestCaseSource(nameof(GetCases))]
        public string? Extract_Value_With_If(Option<string?> option)
        {
            if (option.TryGet(out var value))
            {
                return value;
            }

            throw new UnreachableException();
        }
    }
}
