using Kontur.Options;

namespace Kontur.Tests.Options.Conversion.Linq.Boxed
{
    internal interface IFixtureCase
    {
        public Option<T> GetOption<T>(T value, T constant);
    }
}
