using System;
using System.Collections.Generic;
using System.Linq;
using Kontur.Options;
using NSubstitute;
using NUnit.Framework;

namespace Kontur.Tests.Options.Extraction.Actions
{
    [TestFixture]
    public class OnSome_Should
    {
        private static TestCaseData CreateCallOnSomeIfSomeCase(Func<Option<string>, ICounter, Option<string>> callOnSome)
        {
            return new(callOnSome);
        }

        private static readonly TestCaseData[] CallOnSomeIfSomeCases =
        {
            CreateCallOnSomeIfSomeCase((option, counter) => option.OnSome(counter.Increment)),
            CreateCallOnSomeIfSomeCase((option, counter) => option.OnSome(_ => counter.Increment())),
        };

        [TestCaseSource(nameof(CallOnSomeIfSomeCases))]
        public void Call_OnSome_If_Some(Func<Option<string>, ICounter, Option<string>> callOnSome)
        {
            var counter = Substitute.For<ICounter>();
            var option = Option<string>.Some("foo");

            callOnSome(option, counter);

            counter.Received().Increment();
        }

        [Test]
        public void Pass_Value_If_Some()
        {
            const string expected = "foo";
            var option = Option<string>.Some(expected);
            var consumer = Substitute.For<IConsumer>();

            option.OnSome(value => consumer.Consume(value));

            consumer.Received().Consume(expected);
        }

        private static TestCaseData CreateDoNotCallOnSomeIfNoneCase(Func<Option<string>, Option<string>> assertOnSomeIsNotCalled)
        {
            return new(assertOnSomeIsNotCalled);
        }

        private static void EnsureOnSomeIsNotCalled()
        {
            Assert.Fail("OnSome is called");
        }

        private static readonly TestCaseData[] DoNotCallOnSomeIfNoneCases =
        {
            CreateDoNotCallOnSomeIfNoneCase(option => option.OnSome(EnsureOnSomeIsNotCalled)),
            CreateDoNotCallOnSomeIfNoneCase(option => option.OnSome(_ => EnsureOnSomeIsNotCalled())),
        };

        [TestCaseSource(nameof(DoNotCallOnSomeIfNoneCases))]
        public void Do_Not_Call_OnSome_If_None(Func<Option<string>, Option<string>> assertOnSomeIsNotCalled)
        {
            var option = Option<string>.None();

            assertOnSomeIsNotCalled(option);
        }

        private static readonly Func<Option<int>, Option<int>>[] OnSomeMethods =
        {
            option => option.OnSome(_ => { }),
            option => option.OnSome(() => { }),
        };

        private static readonly Option<int>[] OptionExamples =
        {
            Option<int>.Some(123),
            Option<int>.None(),
        };

        private static readonly IEnumerable<TestCaseData> ReturnSelfCases =
                from option in OptionExamples
                from method in OnSomeMethods
                select new TestCaseData(option, method).Returns(option);

        [TestCaseSource(nameof(ReturnSelfCases))]
        public Option<int> Return_Self(Option<int> option, Func<Option<int>, Option<int>> callOnSome)
        {
            return callOnSome(option);
        }

        private static readonly Func<Option<Child>, Option<Base>>[] UpcastMethods =
        {
            option => option.OnSome<Base>(_ => { }),
            option => option.OnSome<Base>(() => { }),
        };

        private static readonly IEnumerable<TestCaseData> UpcastCases =
            from testCase in UpcastExamples.Get()
            from method in UpcastMethods
            select new TestCaseData(testCase.Option, method).Returns(testCase.Result);

        [TestCaseSource(nameof(UpcastCases))]
        public Option<Base> Return_Self_On_Upcast(
            Option<Child> option,
            Func<Option<Child>, Option<Base>> callOnSome)
        {
            return callOnSome(option);
        }
    }
}