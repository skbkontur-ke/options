using System.Collections.Generic;

namespace Kontur.Tests.Options.Conversion.Linq.Where.SelectMany
{
    internal static class Common
    {
        internal static readonly IEnumerable<SelectCase> Cases = SelectCasesGenerator.Create(2);
    }
}
