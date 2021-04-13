using System.Collections.Generic;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Conversion
{
    [TestFixture]
    internal class Upcast_Should
    {
        private static TestCaseData CreateCase(Option<Child> option, Option<Base> result)
        {
            return new TestCaseData(option).Returns(result);
        }

        private static IEnumerable<TestCaseData> GetCases()
        {
            yield return CreateCase(Option<Child>.None(), Option<Base>.None());

            var child = new Child();
            yield return CreateCase(Option<Child>.Some(child), Option<Base>.Some(child));
        }

        [TestCaseSource(nameof(GetCases))]
        public Option<Base> Process_Option(Option<Child> option)
        {
            return option.Upcast<Base>();
        }
    }
}
