using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Conversion.Linq.Boxed
{
    [TestFixture(typeof(NoneFixtureCase))]
    [TestFixture(typeof(SomeFixtureCase))]
    [TestFixture(typeof(SomeConstantFixtureCase))]
    internal abstract class LinqTestBase<TFixtureCase>
        where TFixtureCase : IFixtureCase, new()
    {
        private protected LinqTestBase()
        {
        }

        protected static readonly TFixtureCase FixtureCase = new();

        protected static Option<int> GetOption(int value) => FixtureCase.GetOption(value);
    }
}
