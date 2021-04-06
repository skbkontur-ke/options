using FluentAssertions;
using Kontur.Options;
using NSubstitute;
using NUnit.Framework;

namespace Kontur.Tests.Options.Extraction
{
    [TestFixture]
    public class OnNone_Should
    {
        [Test]
        public void Call_OnNone_If_None()
        {
            var counter = Substitute.For<ICounter>();
            var option = Option<string>.None();

            option.OnNone(() => counter.Increment());

            counter.Received().Increment();
        }

        [Test]
        public void Do_Not_Call_OnNone_If_Some()
        {
            var option = Option<string>.Some("foo");

            option.OnNone(() => Assert.Fail("OnNone is called"));
        }

        private static TestCaseData CreateReturnSelfCase(Option<int> option)
        {
            return new(option);
        }

        private static readonly TestCaseData[] ReturnSelfCases =
        {
            CreateReturnSelfCase(Option<int>.None()),
            CreateReturnSelfCase(Option<int>.Some(1)),
        };

        [TestCaseSource(nameof(ReturnSelfCases))]
        public void Return_Self_OnNone(Option<int> option)
        {
            var result = option.OnNone(() => { });

            result.Should().BeEquivalentTo(option);
        }
    }
}