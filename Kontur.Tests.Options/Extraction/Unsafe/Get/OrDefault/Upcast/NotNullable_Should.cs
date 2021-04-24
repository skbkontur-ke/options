using System.Collections.Generic;
using System.Linq;
using Kontur.Options;
using Kontur.Options.Unsafe;
using NUnit.Framework;

namespace Kontur.Tests.Options.Extraction.Unsafe.Get.OrDefault.Upcast
{
    [TestFixture]
    internal class NotNullable_Should
    {
        private static IEnumerable<TestCaseData> GetCases()
        {
            return UpcastExamples
                .Get<Base?>(null, value => value)
                .Select(testCase => new TestCaseData(testCase.Option).Returns(testCase.Result));
        }

        [TestCaseSource(nameof(GetCases))]
        public Base? Process_Option(Option<Child> option)
        {
            return option.GetOrDefault<Base>();
        }
    }
}
