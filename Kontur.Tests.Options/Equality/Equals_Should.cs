using System.Collections.Generic;
using System.Linq;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Equality
{
    [TestFixture]
    internal class Equals_Should
    {
        internal static TestCaseData Create<TValue1, TValue2>(Option<TValue1> option1, Option<TValue2> option2) =>
            Common.Create(option1, option2);

        private static readonly TestCaseData[] NonEqualsCases =
        {
            Create(Option.None<string>(), Option.None<int>()),
            Create(Option.None<int>(), Option.None<string>()),

            Create(Option.None<int>(), Option.Some(1)),
            Create(Option.Some(1), Option.None<int>()),
            Create(Option.Some(1), Option.Some(2)),

            Create(Option.Some(2), Option.Some(2.0)),
        };

        private static readonly IEnumerable<TestCaseData> Cases =
            NonEqualsCases
                .Select(testCase => testCase.Returns(false))
                .Concat(Common.CreateEqualsCases().Select(testCase => testCase.Returns(true)));

        [TestCaseSource(nameof(Cases))]
        public bool Compare(object option1, object option2)
        {
            return option1.Equals(option2);
        }
    }
}