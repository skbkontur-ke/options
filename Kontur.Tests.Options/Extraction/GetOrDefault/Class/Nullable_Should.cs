using Kontur.Options;
using Kontur.Options.Unsafe;
using NUnit.Framework;

namespace Kontur.Tests.Options.Extraction.GetOrDefault.Class
{
    [TestFixture]
    internal class Nullable_Should
    {
        private static TestCaseData CreateCase(Option<string?> option, string? result)
        {
            return new TestCaseData(option).Returns(result);
        }

        private static readonly TestCaseData[] Cases =
        {
            CreateCase(Option.None(), null),
            CreateCase(Option.Some<string?>(null), null),
            CreateCase("foo", "foo"),
        };

        [TestCaseSource(nameof(Cases))]
        public string? Process_Option(Option<string?> option)
        {
#if NETFRAMEWORK
#pragma warning disable CS8634 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'class' constraint.
#endif
            return option.GetOrDefault();
#pragma warning restore CS8634 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'class' constraint.
        }
    }
}
