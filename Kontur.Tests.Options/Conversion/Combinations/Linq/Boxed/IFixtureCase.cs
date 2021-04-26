using Kontur.Options;

namespace Kontur.Tests.Options.Conversion.Combinations.Linq.Boxed
{
    internal interface IFixtureCase
    {
        public Option<TValue> GetOption<TValue>(TValue value, TValue constant);
    }
}
