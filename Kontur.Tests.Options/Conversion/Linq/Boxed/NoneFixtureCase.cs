using Kontur.Options;

namespace Kontur.Tests.Options.Conversion.Linq.Boxed
{
    internal class NoneFixtureCase : IFixtureCase
    {
        public Option<int> GetOption(int value) => Option<int>.None();
    }
}
