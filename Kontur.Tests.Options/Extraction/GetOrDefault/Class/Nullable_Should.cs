﻿using Kontur.Options;
using Kontur.Options.Unsafe;
using NUnit.Framework;

namespace Kontur.Tests.Options.Extraction.GetOrDefault.Class
{
    [TestFixture]
    internal class Nullable_Should
    {
        private static TestCaseData CreateCase(Option<string?> option, string? result)
        {
            return new TestCaseData(option).Returns(result);
        }

        private static readonly TestCaseData[] Cases =
        {
            CreateCase(Option.None(), null),
            CreateCase(Option.Some<string?>(null), null),
            CreateCase("foo", "foo"),
        };

        [TestCaseSource(nameof(Cases))]
        public string? Process_Option(Option<string?> option)
        {
            return option.GetOrDefault();
        }
    }
}
