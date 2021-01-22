using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Instantiation.Create_Via_Non_Generic
{
    internal static class Common
    {
        internal static TestCaseData CreateHasValueCase<TValue>(Option<TValue> option, bool hasValue)
        {
            return new TestCaseData(option).Returns(hasValue);
        }
    }
}
