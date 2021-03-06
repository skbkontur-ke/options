﻿using System;
using System.Collections.Generic;
using System.Linq;
using Kontur.Options;
using NSubstitute;
using NUnit.Framework;

namespace Kontur.Tests.Options.Extraction.Actions
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
            return new(option) { ExpectedResult = option };
        }

        private static readonly TestCaseData[] ReturnSelfCases =
        {
            CreateReturnSelfCase(Option<int>.None()),
            CreateReturnSelfCase(Option<int>.Some(1)),
        };

        [TestCaseSource(nameof(ReturnSelfCases))]
        public Option<int> Return_Self_OnNone(Option<int> option)
        {
            return option.OnNone(() => { });
        }

        private static readonly Func<Option<Child>, Option<Base>>[] UpcastMethods =
        {
            option => option.OnNone<Base>(() => { }),
        };

        private static readonly IEnumerable<TestCaseData> UpcastCases =
            from testCase in UpcastExamples.Get()
            from method in UpcastMethods
            select new TestCaseData(testCase.Option, method).Returns(testCase.Result);

        [TestCaseSource(nameof(UpcastCases))]
        public Option<Base> Return_Self_On_Upcast(
            Option<Child> option,
            Func<Option<Child>, Option<Base>> callOnNone)
        {
            return callOnNone(option);
        }
    }
}