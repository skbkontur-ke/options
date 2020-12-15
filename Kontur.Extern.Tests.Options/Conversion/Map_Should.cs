﻿using System.Globalization;
using Kontur.Extern.Options;
using NUnit.Framework;

namespace Kontur.Extern.Tests.Options.Conversion
{
    [TestFixture]
    internal class Map_Should
    {
        private static TestCaseData Create(Option<int> option, Option<string> result)
        {
            return new TestCaseData(option).Returns(result);
        }

        private static readonly TestCaseData[] Cases =
        {
            Create(Option.None(), Option.None()),
            Create(1, "1"),
        };

        [TestCaseSource(nameof(Cases))]
        public Option<string> Process_Value(Option<int> option)
        {
            return option.Map(i => i.ToString(CultureInfo.InvariantCulture));
        }
    }
}
