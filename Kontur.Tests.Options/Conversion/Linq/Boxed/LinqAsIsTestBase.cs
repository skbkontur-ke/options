using System.Collections.Generic;
using NUnit.Framework;

namespace Kontur.Tests.Options.Conversion.Linq.Boxed
{
    internal abstract class LinqAsIsTestBase<TFixtureCase> : LinqTestBase<TFixtureCase>
        where TFixtureCase : IFixtureCase, new()
    {
        protected static IEnumerable<TestCaseData> GenerateCases(int argumentsCount)
            => GenerateCases(argumentsCount, x => x);
    }
}
