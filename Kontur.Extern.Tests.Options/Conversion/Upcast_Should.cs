﻿using System.Collections.Generic;
using Kontur.Extern.Options;
using NUnit.Framework;

namespace Kontur.Extern.Tests.Options.Conversion
{
    [TestFixture]
    internal class Upcast_Should
    {
        private static TestCaseData CreateCase(Option<B> option, Option<A> result)
        {
            return new TestCaseData(option).Returns(result);
        }

        private static IEnumerable<TestCaseData> GetCases()
        {
            yield return CreateCase(Option.None(), Option.None());

            var b = new B();
            yield return CreateCase(b, b);
        }

        [TestCaseSource(nameof(GetCases))]
        public Option<A> Process_Option(Option<B> option)
        {
            return option.Upcast<A>();
        }

        public class A
        {
        }

        public class B : A
        {
        }
    }
}
