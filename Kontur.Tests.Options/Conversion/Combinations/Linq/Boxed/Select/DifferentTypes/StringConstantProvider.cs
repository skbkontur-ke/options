namespace Kontur.Tests.Options.Conversion.Combinations.Linq.Boxed.Select.DifferentTypes
{
    internal class StringConstantProvider : IConstantProvider<string>
    {
        public string Get() => "constant";
    }
}
