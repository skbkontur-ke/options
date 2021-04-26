using Kontur.Options;

namespace Kontur.Tests.Options.Conversion.Combine.Linq.Boxed
{
    internal class NoneFixtureCase : IFixtureCase
    {
        public Option<TValue> GetOption<TValue>(TValue value, TValue constant) => Option<TValue>.None();
    }
}
