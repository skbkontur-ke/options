using System.Collections.Generic;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Conversion
{
    [TestFixture]
    internal class Or_Should
    {
        private static readonly Option<int> None = Option.None();

        private static TestCaseData CreateIntCase(Option<int> option1, Option<int> option2, Option<int> result)
        {
            return new TestCaseData(option1, option2).Returns(result);
        }

        private static readonly TestCaseData[] IntCases =
        {
            CreateIntCase(None, None, None),
            CreateIntCase(1, None, 1),
            CreateIntCase(None, 2, 2),
            CreateIntCase(1, 2, 1),
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
        public void Do_Not_Call_Delegate_On_Some()
        {
            var option = Option.Some(0);

            option.Or(AssertIsNotCalled);
        }

        private static TestCaseData CreateUpcastCase(Option<B> option1, Option<A> option2, Option<A> result)
        {
            return new TestCaseData(option1, option2).Returns(result);
        }

        private static IEnumerable<TestCaseData> GetUpcastCases()
        {
            yield return CreateUpcastCase(Option.None(), Option.None(), Option.None());

            var example = new B();
            yield return CreateUpcastCase(Option.None(), example, example);
            yield return CreateUpcastCase(example, Option.None(), example);
            yield return CreateUpcastCase(example, new B(), example);
        }

        [TestCaseSource(nameof(GetUpcastCases))]
        public Option<A> Upcast_Value(Option<B> option1, Option<A> option2)
        {
            return option1.Or(option2);
        }

        [TestCaseSource(nameof(GetUpcastCases))]
        public Option<A> Upcast_Func(Option<B> option1, Option<A> option2)
        {
            return option1.Or(() => option2);
        }

        public class A
        {
        }

        public class B : A
        {
        }
    }
}
