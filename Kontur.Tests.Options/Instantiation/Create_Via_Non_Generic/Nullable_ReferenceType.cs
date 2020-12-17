using Kontur.Options;
using Kontur.Options.Unsafe;
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
            CreateHasValueCase(Option.Some(null as string), true),
            CreateHasValueCase(Option.Some<string?>("foo"), true),
        };

        [TestCaseSource(nameof(HasValueNullableReferenceTypeCases))]
        public bool Store_HasValue(Option<string?> option)
        {
            return option.HasSome;
        }

        private static TestCaseData CreateStoreValueCase(string? value)
        {
            return Common.CreatePassValueCase(value);
        }

        private static readonly TestCaseData[] PassValueNullableReferenceTypeCases =
        {
            CreateStoreValueCase(null),
            CreateStoreValueCase("foo"),
        };

        [TestCaseSource(nameof(PassValueNullableReferenceTypeCases))]
        public string? Store_Value(Option<string?> option)
        {
            return option.GetOrThrow();
        }
    }
}
