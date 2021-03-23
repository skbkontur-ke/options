using Kontur.Options;

namespace Kontur.Tests.Options.Conversion.Linq.Boxed
{
    internal class SomeConstantFixtureCase : IFixtureCase
    {
        public Option<int> GetOption(int value) => Option<int>.Some(88888);
    }
}
