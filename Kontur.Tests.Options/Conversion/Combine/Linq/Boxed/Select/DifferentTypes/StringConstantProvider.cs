namespace Kontur.Tests.Options.Conversion.Combine.Linq.Boxed.Select.DifferentTypes
{
    internal class StringConstantProvider : IConstantProvider<string>
    {
        public string Get() => "constant";
    }
}
