namespace Kontur.Tests.Options.Conversion.Linq.Boxed
{
    internal interface IConstantProvider<out TValue>
    {
        TValue Get();
    }
}
