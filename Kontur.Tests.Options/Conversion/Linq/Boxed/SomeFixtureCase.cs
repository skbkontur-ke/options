using Kontur.Options;

namespace Kontur.Tests.Options.Conversion.Linq.Boxed
{
    internal class SomeFixtureCase : IFixtureCase
    {
        public Option<int> GetResult(int value) => Option<int>.Some(value);
    }
}
