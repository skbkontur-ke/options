﻿using FluentAssertions;
using Kontur.Extern.Options;
using NSubstitute;
using NUnit.Framework;

namespace Kontur.Extern.Tests.Options.Extraction
{
    [TestFixture]
    public class Switch_Method_Should
    {
        [Test]
        public void Call_OnSome_If_Some()
        {
            var counter = Substitute.For<ICounter>();
            var option = Option.Some("foo");

            option.Switch(_ => counter.Increment(), () => { });

            counter.Received().Increment();
        }

        [Test]
        public void Pass_Value_OnSome()
        {
            const string expected = "foo";
            var option = Option.Some(expected);

            option.Switch(value => value.Should().Be(expected), () => { });
        }

        [Test]
        public void Call_OnNone_If_None()
        {
            var counter = Substitute.For<ICounter>();
            var option = Option.None<string>();

            option.Switch(_ => { }, () => counter.Increment());

            counter.Received().Increment();
        }

        [Test]
        public void Do_Not_Call_OnSome_If_None()
        {
            var option = Option.None<string>();

            option.Switch(_ => Assert.Fail("OnSome called"), () => { });
        }

        [Test]
        public void Do_Not_Call_On_None_If_Some()
        {
            var option = Option.Some("foo");

            option.Switch(_ => { }, () => Assert.Fail("OnNone called"));
        }

        private static TestCaseData CreateReturnSelfCase(Option<int> option)
        {
            return new TestCaseData(option);
        }

        private static readonly TestCaseData[] ReturnSelfCases =
        {
            CreateReturnSelfCase(Option.None()),
            CreateReturnSelfCase(1),
        };

        [TestCaseSource(nameof(ReturnSelfCases))]
        public void Return_Self(Option<int> option)
        {
            var result = option.Switch(_ => { }, () => { });

            result.Should().BeEquivalentTo(option);
        }
    }
}