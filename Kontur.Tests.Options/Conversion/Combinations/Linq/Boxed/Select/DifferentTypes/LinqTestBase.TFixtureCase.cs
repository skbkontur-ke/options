﻿using System.Collections.Generic;
using System.Globalization;
using NUnit.Framework;

namespace Kontur.Tests.Options.Conversion.Combinations.Linq.Boxed.Select.DifferentTypes
{
   internal abstract class LinqTestBase<TFixtureCase> : LinqTestBase<TFixtureCase, StringConstantProvider, string>
        where TFixtureCase : IFixtureCase, new()
    {
        protected static IEnumerable<TestCaseData> CreateSelectCases(int argumentsCount)
        {
            return CreateSelectCases(argumentsCount, ConvertToString);
        }

        protected static string ConvertToString(int value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }
    }
}
