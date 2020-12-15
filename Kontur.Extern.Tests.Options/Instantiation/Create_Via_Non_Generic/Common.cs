﻿using Kontur.Extern.Options;
using NUnit.Framework;

namespace Kontur.Extern.Tests.Options.Instantiation.Create_Via_Non_Generic
{
    internal static class Common
    {
        internal static TestCaseData CreateHasValueCase<TValue>(Option<TValue> option, bool hasValue)
        {
            return new TestCaseData(option).Returns(hasValue);
        }

        internal static TestCaseData CreatePassValueCase<TValue>(TValue value)
        {
            return new TestCaseData(Option.Some(value)).Returns(value);
        }
    }
}
