namespace Kontur.Tests.Options.Conversion.Combinations.Linq.Boxed
{
    internal interface IConstantProvider<out TValue>
    {
        TValue Get();
    }
}
