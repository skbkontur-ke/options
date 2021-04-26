namespace Kontur.Tests.Options.Conversion.Combine.Linq.Boxed
{
    internal interface IConstantProvider<out TValue>
    {
        TValue Get();
    }
}
