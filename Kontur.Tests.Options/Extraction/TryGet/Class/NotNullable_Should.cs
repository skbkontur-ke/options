using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Extraction.TryGet.Class
{
    [TestFixture]
    internal class NotNullable_Should
    {
        private static TestCaseData Create(Option<string> option, bool success)
        {
            return Common.CreateReturnBooleanCase(option, success);
        }

        private static readonly TestCaseData[] Cases =
        {
            Create(Option<string>.None(), false),
            Create(Option<string>.Some("foo"), true),
        };

        [TestCaseSource(nameof(Cases))]
        public bool Return_Boolean(Option<string> option)
        {
            return option.TryGet(out _);
        }

        private static readonly TestCaseData[] GetCases =
        {
            new TestCaseData(Option<string>.Some("foo")).Returns("foo"),
        };

        [TestCaseSource(nameof(GetCases))]

        public string? Extract_Value(Option<string> option)
        {
            _ = option.TryGet(out var value);

            return value;
        }

        [TestCaseSource(nameof(GetCases))]
        public string Extract_Value_With_If(Option<string> option)
        {
            if (option.TryGet(out var value))
            {
#if NETFRAMEWORK
#pragma warning disable 8603
#endif
                return value;
#pragma warning restore 8603
            }

            throw new UnreachableException();
        }
    }
}