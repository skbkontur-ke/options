using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Extraction.TryGet.Struct
{
    [TestFixture]
    internal class Nullable_Should
    {
        private static TestCaseData CreateReturnBooleanCase(Option<int?> option, bool success)
        {
            return Common.CreateReturnBooleanCase(option, success);
        }

        private static readonly TestCaseData[] ReturnBooleanCases =
        {
            CreateReturnBooleanCase(Option<int?>.None(), false),
            CreateReturnBooleanCase(Option<int?>.Some(null), true),
            CreateReturnBooleanCase(Option<int?>.Some(1), true),
        };

        [TestCaseSource(nameof(ReturnBooleanCases))]
        public bool Return_Boolean(Option<int?> option)
        {
            return option.TryGet(out _);
        }

        private static TestCaseData CreateGetCase(int? expectedValue)
        {
            return new(Option<int?>.Some(expectedValue)) { ExpectedResult = expectedValue };
        }

        private static readonly TestCaseData[] GetCases =
        {
            CreateGetCase(null),
            CreateGetCase(10),
        };

        [TestCaseSource(nameof(GetCases))]
        public int? Extract_Value(Option<int?> option)
        {
            _ = option.TryGet(out var value);

            return value;
        }

        [TestCaseSource(nameof(GetCases))]
        public int? Extract_Value_With_If(Option<int?> option)
        {
            if (option.TryGet(out var value))
            {
                return value;
            }

            throw new UnreachableException();
        }
    }
}
