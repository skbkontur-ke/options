using System.Globalization;
using FluentAssertions;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Conversion.Combinations
{
    [TestFixture]
    internal class Select_Should
    {
        private static TestCaseData Create(Option<int> option, Option<string> result)
        {
            return new(option) { ExpectedResult = result };
        }

        private static readonly TestCaseData[] Cases =
        {
            Create(Option<int>.None(), Option<string>.None()),
            Create(Option<int>.Some(1), Option<string>.Some("1")),
        };

        [TestCaseSource(nameof(Cases))]
        public Option<string> Process_Value(Option<int> option)
        {
            return option.Select(i => i.ToString(CultureInfo.InvariantCulture));
        }

        [TestCaseSource(nameof(Cases))]
        public Option<string> Process_Result(Option<int> option)
        {
            return option.Select(i => Option<string>.Some(i.ToString(CultureInfo.InvariantCulture)));
        }

        [Test]
        public void Convert_Some_To_None()
        {
            var option = Option<string>.Some("unused");

            var result = option.Select(_ => Option<string>.None());

            result.Should().BeEquivalentTo(Option<string>.None());
        }
    }
}
