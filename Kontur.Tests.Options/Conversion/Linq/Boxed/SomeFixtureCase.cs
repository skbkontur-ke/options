using Kontur.Options;

namespace Kontur.Tests.Options.Conversion.Linq.Boxed
{
    internal class SomeFixtureCase : IFixtureCase
    {
        public Option<TValue> GetOption<TValue>(TValue value, TValue constant) => Option<TValue>.Some(value);
    }
}
