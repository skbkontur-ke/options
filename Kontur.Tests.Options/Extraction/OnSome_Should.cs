using FluentAssertions;
using Kontur.Options;
using NSubstitute;
using NUnit.Framework;

namespace Kontur.Tests.Options.Extraction
{
    [TestFixture]
    public class OnSome_Should
    {
        [Test]
        public void Call_OnSome_With_Argument_If_Some()
        {
            var counter = Substitute.For<ICounter>();
            var option = Option.Some("foo");

            option.OnSome(_ => counter.Increment());

            counter.Received().Increment();
        }

        [Test]
        public void Pass_Value_If_Some()
        {
            const string expected = "foo";
            var option = Option.Some(expected);

            option.OnSome(value => value.Should().Be(expected));
        }

        [Test]
        public void Do_Not_Call_OnSome_With_Argument_If_None()
        {
            var option = Option.None<string>();

            option.OnSome(_ => Assert.Fail("OnSome is called"));
        }

        [Test]
        public void Call_OnSome_Without_Argument_If_Some()
        {
            var counter = Substitute.For<ICounter>();
            var option = Option.Some("foo");

            option.OnSome(() => counter.Increment());

            counter.Received().Increment();
        }

        [Test]
        public void Do_Not_Call_OnSome_Without_Argument_If_None()
        {
            var option = Option.None<string>();

            option.OnSome(() => Assert.Fail("OnSome is called"));
        }

        private static TestCaseData CreateReturnSelfCase(Option<int> option)
        {
            return new(option);
        }

        private static readonly TestCaseData[] ReturnSelfCases =
        {
            CreateReturnSelfCase(Option.None()),
            CreateReturnSelfCase(1),
        };

        [TestCaseSource(nameof(ReturnSelfCases))]
        public void Return_Self_OnSome_With_Argument(Option<int> option)
        {
            var result = option.OnSome(_ => { });

            result.Should().BeEquivalentTo(option);
        }

        [TestCaseSource(nameof(ReturnSelfCases))]
        public void Return_Self_OnSome_Without_Argument(Option<int> option)
        {
            var result = option.OnSome(() => { });

            result.Should().BeEquivalentTo(option);
        }
    }
}