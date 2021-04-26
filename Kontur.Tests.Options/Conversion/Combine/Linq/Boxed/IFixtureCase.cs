using Kontur.Options;

namespace Kontur.Tests.Options.Conversion.Combine.Linq.Boxed
{
    internal interface IFixtureCase
    {
        public Option<TValue> GetOption<TValue>(TValue value, TValue constant);
    }
}
