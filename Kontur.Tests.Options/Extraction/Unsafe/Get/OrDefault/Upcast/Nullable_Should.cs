using System.Collections.Generic;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Extraction.Unsafe.Get.OrDefault.Upcast
{
    [TestFixture]
    internal class Nullable_Should
    {
        private static TestCaseData CreateCase(Option<Child?> option, Base? result)
        {
            return new(option) { ExpectedResult = result };
        }

        private static IEnumerable<TestCaseData> GetCases()
        {
            yield return CreateCase(Option<Child?>.None(), null);
            yield return CreateCase(Option<Child?>.Some(null), null);

            Child child = new();
            yield return CreateCase(Option<Child?>.Some(child), child);
        }

        [TestCaseSource(nameof(GetCases))]
        public Base? Process_Option(Option<Child?> option)
        {
            return option.GetOrDefault<Base?>();
        }
    }
}
