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
            CreateHasValueCase(Option.Some<int?>(null), true),
            CreateHasValueCase(Option.Some<int?>(10), true),
        };

        [TestCaseSource(nameof(HasValueNullableValueTypeCases))]
        public bool HasValue(Option<int?> option)
        {
            return option.HasSome;
        }

        [TestCase(null, ExpectedResult = null)]
        [TestCase(10, ExpectedResult = 10)]
        public int? Store_Value(int? value)
        {
            var option = Option.Some(value);

            return option.GetOrThrow();
        }
    }
}
