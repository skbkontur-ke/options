using System.Collections.Generic;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Extraction
{
    [TestFixture]
    internal class GetOrElse_Should
    {
        private static TestCaseData CreateCase(Option<int> option, int value, int result)
        {
            return new TestCaseData(option, value).Returns(result);
        }

        private static readonly TestCaseData[] Cases =
        {
            CreateCase(Option.None(), 2, 2),
            CreateCase(1, 2, 1),
        };

        [TestCaseSource(nameof(Cases))]
        public int Process_Option(Option<int> option, int value)
        {
            return option.GetOrElse(value);
        }

        [TestCaseSource(nameof(Cases))]
        public int Process_Func(Option<int> option, int value)
        {
            return option.GetOrElse(() => value);
        }

        [Test]
        public void Do_Not_Call_Delegate_On_Some()
        {
            var options = Option.Some(0);

            options.GetOrElse(ThrowError);
        }

        private static int ThrowError()
        {
            Assert.Fail("Backup value factory should not be called on Some");
            throw new UnreachableException();
        }

        private static TestCaseData CreateUpcastCase(Option<B> option, A defaultValue, A result)
        {
            return new TestCaseData(option, defaultValue).Returns(result);
        }

        private static IEnumerable<TestCaseData> GetUpcastCases()
        {
            var some = new B();
            yield return CreateUpcastCase(Option.None(), some, some);
            yield return CreateUpcastCase(some, new B(), some);
        }

        [TestCaseSource(nameof(GetUpcastCases))]
        public A Upcast_Value(Option<B> option, A defaultValue)
        {
            return option.GetOrElse(defaultValue);
        }

        [TestCaseSource(nameof(GetUpcastCases))]
        public A Upcast_Func(Option<B> option1, A defaultValue)
        {
            return option1.GetOrElse(() => defaultValue);
        }

        public class A
        {
        }

        public class B : A
        {
        }
    }
}
