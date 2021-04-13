using System.Collections.Generic;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Conversion
{
    [TestFixture]
    internal class Or_Should
    {
        private static readonly Option<int> None = Option<int>.None();

        private static TestCaseData CreateIntCase(Option<int> option1, Option<int> option2, Option<int> result)
        {
            return new TestCaseData(option1, option2).Returns(result);
        }

        private static readonly TestCaseData[] IntCases =
        {
            CreateIntCase(None, None, None),
            CreateIntCase(Option<int>.Some(1), None, Option<int>.Some(1)),
            CreateIntCase(None, Option<int>.Some(2), Option<int>.Some(2)),
            CreateIntCase(Option<int>.Some(1), Option<int>.Some(2), Option<int>.Some(1)),
        };

        [TestCaseSource(nameof(IntCases))]
        public Option<int> Process_Value(Option<int> option1, Option<int> option2)
        {
            return option1.Or(option2);
        }

        [TestCaseSource(nameof(IntCases))]
        public Option<int> Process_Func(Option<int> option1, Option<int> option2)
        {
            return option1.Or(() => option2);
        }

        private static Option<int> AssertIsNotCalled()
        {
            Assert.Fail("Backup value factory should not be called on Some");
            throw new UnreachableException();
        }

        [Test]
        public void Do_Not_Call_Delegate_If_Some()
        {
            var option = Option<int>.Some(0);

            option.Or(AssertIsNotCalled);
        }

        private static TestCaseData CreateUpcastCase(Option<Child> option1, Option<Base> option2, Option<Base> result)
        {
            return new TestCaseData(option1, option2).Returns(result);
        }

        private static IEnumerable<TestCaseData> GetUpcastCases()
        {
            yield return CreateUpcastCase(Option<Child>.None(), Option<Base>.None(), Option<Base>.None());

            var example = new Child();
            yield return CreateUpcastCase(Option<Child>.None(), Option<Base>.Some(example), Option<Base>.Some(example));
            yield return CreateUpcastCase(Option<Child>.Some(example), Option<Base>.None(), Option<Base>.Some(example));
            yield return CreateUpcastCase(Option<Child>.Some(example), Option<Base>.Some(new Child()), Option<Base>.Some(example));
        }

        [TestCaseSource(nameof(GetUpcastCases))]
        public Option<Base> Upcast_Value(Option<Child> option1, Option<Base> option2)
        {
            return option1.Or(option2);
        }

        [TestCaseSource(nameof(GetUpcastCases))]
        public Option<Base> Upcast_Func(Option<Child> option1, Option<Base> option2)
        {
            return option1.Or(() => option2);
        }
    }
}
