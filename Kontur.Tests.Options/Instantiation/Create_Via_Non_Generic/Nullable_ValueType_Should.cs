using Kontur.Options;
using Kontur.Options.Unsafe;
using NUnit.Framework;

namespace Kontur.Tests.Options.Instantiation.Create_Via_Non_Generic
{
    [TestFixture]
    internal class Nullable_ValueType_Should
    {
        private static TestCaseData CreateHasValueCase(Option<int?> option, bool hasValue)
        {
            return Common.CreateHasValueCase(option, hasValue);
        }

        private static readonly TestCaseData[] HasValueNullableValueTypeCases =
        {
            CreateHasValueCase(Option.None<int?>(), false),
            CreateHasValueCase(Option.Some(null as int?), true),
            CreateHasValueCase(Option.Some(10 as int?), true),
        };

        [TestCaseSource(nameof(HasValueNullableValueTypeCases))]
        public bool Store_HasValue(Option<int?> option)
        {
            return option.HasSome;
        }

        private static TestCaseData CreateStoreValueCase(int? value)
        {
            return Common.CreatePassValueCase(value);
        }

        private static readonly TestCaseData[] PassValueNullableValueTypeCases =
        {
            CreateStoreValueCase(null),
            CreateStoreValueCase(10),
        };

        [TestCaseSource(nameof(PassValueNullableValueTypeCases))]
        public int? Store_Value(Option<int?> option)
        {
            return option.GetOrThrow();
        }
    }
}
