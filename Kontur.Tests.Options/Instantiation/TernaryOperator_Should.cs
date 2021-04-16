using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Instantiation
{
    [TestFixture]
    internal class TernaryOperator_Should
    {
        private const int SomeValue = 7;

        private static TestCaseData CreateCase(bool flag, Option<int> result)
        {
            return new(flag) { ExpectedResult = result };
        }

        private static readonly TestCaseData[] Cases =
        {
            CreateCase(true, SomeValue),
            CreateCase(false, Option.None()),
        };

        [TestCaseSource(nameof(Cases))]
        public Option<int> Create_Via_Other_Argument_Implicit_Conversion(bool flag)
        {
            var option = flag
                ? Option.Some(SomeValue)
                : Option.None();
            return option;
        }

        [TestCaseSource(nameof(Cases))]
        public Option<int> Create_Via_Target_Type_Inference(bool flag)
        {
            return flag
                ? SomeValue
                : Option.None();
        }
    }
}
