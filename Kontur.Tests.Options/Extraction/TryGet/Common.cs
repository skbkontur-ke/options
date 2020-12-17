using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Extraction.TryGet
{
    internal static class Common
    {
        internal static TestCaseData CreateReturnBooleanCase<T>(Option<T> option, bool success)
        {
            return new TestCaseData(option).Returns(success);
        }
    }
}
