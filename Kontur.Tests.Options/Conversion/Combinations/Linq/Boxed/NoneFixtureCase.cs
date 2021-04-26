using Kontur.Options;

namespace Kontur.Tests.Options.Conversion.Combinations.Linq.Boxed
{
    internal class NoneFixtureCase : IFixtureCase
    {
        public Option<TValue> GetOption<TValue>(TValue value, TValue constant) => Option<TValue>.None();
    }
}
