using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Extraction.TryGet.Struct
{
    [TestFixture]
    internal class NotNullable_Should
    {
        private static TestCaseData Create(Option<int> option, bool success)
        {
            return Common.CreateReturnBooleanCase(option, success);
        }

        private static readonly TestCaseData[] Cases =
        {
            Create(Option.None(), false),
            Create(1, true),
        };

        [TestCaseSource(nameof(Cases))]
        public bool Return_Boolean(Option<int> option)
        {
            return option.TryGet(out _);
        }

        private static readonly TestCaseData[] GetCases =
        {
            new TestCaseData(Option.Some(10)).Returns(10),
        };

        [TestCaseSource(nameof(GetCases))]
        public int Extract_Value(Option<int> option)
        {
            _ = option.TryGet(out var value);

            return value;
        }

        [TestCaseSource(nameof(GetCases))]
        public int Extract_Value_With_If(Option<int> option)
        {
            if (option.TryGet(out var value))
            {
                return value;
            }

            throw new UnreachableException();
        }
    }
}
