using System.Globalization;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Conversion
{
    [TestFixture]
    internal class Select_Should
    {
        private static TestCaseData Create(Option<int> option, Option<string> result)
        {
            return new TestCaseData(option).Returns(result);
        }

        private static readonly TestCaseData[] Cases =
        {
            Create(Option.None(), Option.None()),
            Create(1, "1"),
        };

        [TestCaseSource(nameof(Cases))]
        public Option<string> Process_Value(Option<int> option)
        {
            return option.Select(i => i.ToString(CultureInfo.InvariantCulture));
        }
    }
}
