using Kontur.Options;
using Kontur.Options.Unsafe;
using NUnit.Framework;

namespace Kontur.Tests.Options.Extraction.GetOrDefault.Struct
{
    [TestFixture]
    internal class Nullable_Should
    {
        private static TestCaseData CreateCase(Option<int?> option, int? result)
        {
            return new TestCaseData(option).Returns(result);
        }

        private static readonly TestCaseData[] Cases =
        {
            CreateCase(Option.None(), null),
            CreateCase(Option.Some<int?>(null), null),
            CreateCase(1, 1),
        };

        [TestCaseSource(nameof(Cases))]
        public int? Process_Option(Option<int?> option)
        {
            return option.GetOrDefault();
        }
    }
}
