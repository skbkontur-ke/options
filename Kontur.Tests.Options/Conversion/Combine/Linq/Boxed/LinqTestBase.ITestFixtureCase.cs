using System.Collections.Generic;
using NUnit.Framework;

namespace Kontur.Tests.Options.Conversion.Combine.Linq.Boxed
{
   internal abstract class LinqTestBase<TFixtureCase> : LinqTestBase<TFixtureCase, IntConstantProvider, int>
        where TFixtureCase : IFixtureCase, new()
    {
        protected static IEnumerable<TestCaseData> CreateSelectCases(int argumentsCount)
        {
            return CreateSelectCases(argumentsCount, x => x);
        }
    }
}
