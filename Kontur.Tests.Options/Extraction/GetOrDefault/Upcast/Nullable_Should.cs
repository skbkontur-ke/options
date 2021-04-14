using System.Collections.Generic;
using Kontur.Options;
using Kontur.Options.Unsafe;
using NUnit.Framework;

namespace Kontur.Tests.Options.Extraction.GetOrDefault.Upcast
{
    [TestFixture]
    internal class Nullable_Should
    {
        private static TestCaseData CreateCase(Option<Child?> option, Base? result)
        {
            return new TestCaseData(option).Returns(result);
        }

        private static IEnumerable<TestCaseData> GetCases()
        {
            yield return CreateCase(Option<Child?>.None(), null);
            yield return CreateCase(Option<Child?>.Some(null), null);

            var child = new Child();
            yield return CreateCase(Option<Child?>.Some(child), child);
        }

        [TestCaseSource(nameof(GetCases))]
        public Base? Process_Option(Option<Child?> option)
        {
            return option.GetOrDefault<Base?>();
        }
    }
}
