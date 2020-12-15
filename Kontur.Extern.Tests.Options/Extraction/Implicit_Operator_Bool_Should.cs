using Kontur.Extern.Options;
using NUnit.Framework;

namespace Kontur.Extern.Tests.Options.Extraction
{
    [TestFixture]
    internal class Implicit_Operator_Bool_Should
    {
        private static TestCaseData CreateCase(Option<int> option, bool hasValue)
        {
            return new TestCaseData(option).Returns(hasValue);
        }

        private static readonly TestCaseData[] Cases =
        {
            CreateCase(Option.None<int>(), false),
            CreateCase(Option.Some(10), true),
        };

        [TestCaseSource(nameof(Cases))]
        public bool Convert(Option<int> option)
        {
            return option;
        }
    }
}