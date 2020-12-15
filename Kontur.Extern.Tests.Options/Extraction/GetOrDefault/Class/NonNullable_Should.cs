﻿using Kontur.Extern.Options;
using Kontur.Extern.Options.Unsafe;
using NUnit.Framework;

namespace Kontur.Extern.Tests.Options.Extraction.GetOrDefault.Class
{
    [TestFixture]
    internal class NonNullable_Should
    {
        private static TestCaseData CreateCase(Option<string> option, string? result)
        {
            return new TestCaseData(option).Returns(result);
        }

        private static readonly TestCaseData[] Cases =
        {
            CreateCase(Option.None(), null),
            CreateCase("foo", "foo"),
        };

        [TestCaseSource(nameof(Cases))]
        public string? Process_Option(Option<string> option)
        {
            return option.GetOrDefault();
        }
    }
}
