﻿using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Extraction.Get.OrDefault.Struct
{
    [TestFixture]
    internal class NotNullable_Should
    {
        private static TestCaseData CreateCase(Option<int> option, int result)
        {
            return new(option) { ExpectedResult = result };
        }

        private static readonly TestCaseData[] Cases =
        {
            CreateCase(Option<int>.None(), 0),
            CreateCase(Option<int>.Some(1), 1),
        };

        [TestCaseSource(nameof(Cases))]
        public int Process_Option(Option<int> option)
        {
            return option.GetOrDefault();
        }
    }
}
