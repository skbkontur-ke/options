using System.Collections.Generic;
using System.Linq;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Extraction
{
    [TestFixture]
    internal class GetValues_Should
    {
        private static TestCaseData CreateCase(Option<int> option, IEnumerable<int> results)
        {
            return new(option) { ExpectedResult = results };
        }

        private static readonly TestCaseData[] Cases =
        {
            CreateCase(Option<int>.Some(2), new[] { 2 }),
            CreateCase(Option<int>.None(), Enumerable.Empty<int>()),
        };

        [TestCaseSource(nameof(Cases))]
        public IEnumerable<int> Enumerated_With_Type_Safety(Option<int> option)
        {
            return option.GetValues();
        }

        private static IEnumerable<TestCaseData> GetUpcastCases()
        {
            return UpcastExamples
                .Get(Enumerable.Empty<Base>(), value => new[] { value })
                .Select(testCase => new TestCaseData(testCase.Option).Returns(testCase.Result));
        }

        [TestCaseSource(nameof(GetUpcastCases))]
        public IEnumerable<Base> Upcast(Option<Child> option)
        {
            return option.GetValues<Base>();
        }
    }
}