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
            return new(option) { ExpectedResult = result };
        }

        private static readonly TestCaseData[] Cases =
        {
            CreateCase(Option<string?>.None(), null),
            CreateCase(Option<string?>.Some(null), null),
            CreateCase(Option<string?>.Some("foo"), "foo"),
        };

        [TestCaseSource(nameof(Cases))]
        public string? Process_Option(Option<string?> option)
        {
            return option.GetOrDefault();
        }
    }
}
