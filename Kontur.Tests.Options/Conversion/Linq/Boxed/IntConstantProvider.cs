namespace Kontur.Tests.Options.Conversion.Linq.Boxed
{
    internal class IntConstantProvider : IConstantProvider<int>
    {
        public int Get() => 8888;
    }
}
