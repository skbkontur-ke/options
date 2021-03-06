﻿using System.Collections.Generic;
using System.Linq;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Extraction.Get
{
    [TestFixture]
    internal class Enumerator_Should
    {
        private static TestCaseData CreateCase(Option<int> option, IEnumerable<int> results)
        {
            return new(option) { ExpectedResult = results };
        }

        private static readonly TestCaseData[] Cases =
        {
            CreateCase(Option<int>.Some(2), new[] { 2 }),
            CreateCase(Option<int>.None(), Enumerable.Empty<int>()),
        };

        [TestCaseSource(nameof(Cases))]
        public IEnumerable<int> Foreach_With_Type_Safety(Option<int> option)
        {
            foreach (var value in option)
            {
                yield return value;
            }
        }
    }
}