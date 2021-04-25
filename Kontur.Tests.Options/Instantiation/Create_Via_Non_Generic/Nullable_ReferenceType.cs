using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Instantiation.Create_Via_Non_Generic
{
    [TestFixture]
    internal class Nullable_ReferenceType
    {
        private static TestCaseData CreateHasValueCase(Option<string?> option, bool hasValue)
        {
            return Common.CreateHasValueCase(option, hasValue);
        }

        private static readonly TestCaseData[] HasValueNullableReferenceTypeCases =
        {
            CreateHasValueCase(Option.None<string?>(), false),
            CreateHasValueCase(Option.Some<string?>(null), true),
            CreateHasValueCase(Option.Some<string?>("foo"), true),
        };

        [TestCaseSource(nameof(HasValueNullableReferenceTypeCases))]
        public bool HasValue(Option<string?> option)
        {
            return option.HasSome;
        }

        [TestCase(null, ExpectedResult = null)]
        [TestCase("foo", ExpectedResult = "foo")]
        public string? Store_Value(string? value)
        {
            var option = Option.Some(value);

            return option.GetOrThrow();
        }
    }
}
