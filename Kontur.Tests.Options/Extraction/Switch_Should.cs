using System;
using FluentAssertions;
using Kontur.Options;
using NSubstitute;
using NUnit.Framework;

namespace Kontur.Tests.Options.Extraction
{
    [TestFixture]
    public class Switch_Should
    {
        private static TestCaseData CreateCallSwitchWithCounterCase(Func<Option<string>, ICounter, Option<string>> callSwitch)
        {
            return new(callSwitch);
        }

        private static readonly TestCaseData[] CallOnNoneIfNoneCases =
        {
            CreateCallSwitchWithCounterCase((option, counter) => option.Switch(counter.Increment, () => { })),
            CreateCallSwitchWithCounterCase((option, counter) => option.Switch(counter.Increment, _ => { })),
        };

        [TestCaseSource(nameof(CallOnNoneIfNoneCases))]
        public void Call_OnNone_If_None(Func<Option<string>, ICounter, Option<string>> callSwitch)
        {
            var counter = Substitute.For<ICounter>();
            var option = Option.None<string>();

            callSwitch(option, counter);

            counter.Received().Increment();
        }

        private static readonly TestCaseData[] CallOnSomeIfSomeCases =
        {
            CreateCallSwitchWithCounterCase((option, counter) => option.Switch(() => { }, counter.Increment)),
            CreateCallSwitchWithCounterCase((option, counter) => option.Switch(() => { }, _ => counter.Increment())),
        };

        [TestCaseSource(nameof(CallOnSomeIfSomeCases))]
        public void Call_OnSome_If_Some(Func<Option<string>, ICounter, Option<string>> callSwitch)
        {
            var counter = Substitute.For<ICounter>();
            var option = Option.Some("foo");

            callSwitch(option, counter);

            counter.Received().Increment();
        }

        private static TestCaseData CreateCallSwitchCase<TValue>(Func<Option<TValue>, Option<TValue>> assertOnNoneIsNotCalled)
        {
            return new(assertOnNoneIsNotCalled);
        }

        private static TestCaseData CreateDoNotCallOnSomeIfNoneCase(Func<Option<string>, Option<string>> assertOnSomeIsNotCalled)
        {
            return CreateCallSwitchCase(assertOnSomeIsNotCalled);
        }

        private static void EnsureOnSomeIsNotCalled()
        {
            Assert.Fail("OnSome is called");
        }

        private static readonly TestCaseData[] DoNotCallOnSomeIfNoneCases =
        {
            CreateDoNotCallOnSomeIfNoneCase(option => option.Switch(() => { }, EnsureOnSomeIsNotCalled)),
            CreateDoNotCallOnSomeIfNoneCase(option => option.Switch(() => { }, _ => EnsureOnSomeIsNotCalled())),
        };

        [TestCaseSource(nameof(DoNotCallOnSomeIfNoneCases))]
        public void Do_Not_Call_OnSome_If_None(Func<Option<string>, Option<string>> assertOnSomeIsNotCalled)
        {
            var option = Option.None<string>();

            assertOnSomeIsNotCalled(option);
        }

        private static TestCaseData CreateDoNotCallOnNoneIfSomeCase(Func<Option<string>, Option<string>> assertOnNoneIsNotCalled)
        {
            return CreateCallSwitchCase(assertOnNoneIsNotCalled);
        }

        private static void EnsureOnNoneIsNotCalled()
        {
            Assert.Fail("OnNone is called");
        }

        private static readonly TestCaseData[] DoNotCallOnNoneIfSomeCases =
        {
            CreateDoNotCallOnNoneIfSomeCase(option => option.Switch(EnsureOnNoneIsNotCalled, _ => { })),
            CreateDoNotCallOnNoneIfSomeCase(option => option.Switch(EnsureOnNoneIsNotCalled, () => { })),
        };

        [TestCaseSource(nameof(DoNotCallOnNoneIfSomeCases))]
        public void Do_Not_Call_On_None_If_Some(Func<Option<string>, Option<string>> assertOnNoneIsNotCalled)
        {
            var option = Option.Some("foo");

            assertOnNoneIsNotCalled(option);
        }

        private static TestCaseData CreateReturnSelfCase(Func<Option<int>, Option<int>> callSwitch)
        {
            return CreateCallSwitchCase(callSwitch);
        }

        private static readonly TestCaseData[] ReturnSelfCases =
        {
            CreateReturnSelfCase(option => option.Switch(() => { }, _ => { })),
            CreateReturnSelfCase(option => option.Switch(() => { }, () => { })),
        };

        [TestCaseSource(nameof(ReturnSelfCases))]
        public void Return_Self_On_None(Func<Option<int>, Option<int>> callSwitch)
        {
            var option = Option<int>.None();

            var result = callSwitch(option);

            result.Should().BeEquivalentTo(option);
        }

        [TestCaseSource(nameof(ReturnSelfCases))]
        public void Return_Self_On_Some(Func<Option<int>, Option<int>> callSwitch)
        {
            var option = Option<int>.Some(123);

            var result = callSwitch(option);

            result.Should().BeEquivalentTo(option);
        }

        [Test]
        public void Pass_Value_OnSome()
        {
            const string expected = "foo";
            var option = Option.Some(expected);
            var consumer = Substitute.For<IConsumer>();

            option.Switch(() => { }, value => consumer.Consume(value));

            consumer.Received().Consume(expected);
        }
    }
}