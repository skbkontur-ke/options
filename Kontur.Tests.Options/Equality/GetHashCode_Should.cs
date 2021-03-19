using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace Kontur.Tests.Options.Equality
{
    [TestFixture]
    internal class GetHashCode_Should
    {
        public static readonly IEnumerable<TestCaseData> Cases = Common.CreateEqualsCases();

        [TestCaseSource(nameof(Cases))]
        public void Calculate(object option1, object option2)
        {
            var hashCode1 = option1.GetHashCode();
            var hashCode2 = option2.GetHashCode();

            hashCode1.Should().Be(hashCode2);
        }
    }
}