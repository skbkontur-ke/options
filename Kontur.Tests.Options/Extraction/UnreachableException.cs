using NUnit.Framework;

namespace Kontur.Tests.Options.Extraction
{
    internal class UnreachableException : AssertionException
    {
        public UnreachableException()
            : base("Branch should be unreachable")
        {
        }
    }
}
