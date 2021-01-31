using NUnit.Framework;

namespace Kontur.Tests.Options
{
    internal class UnreachableException : AssertionException
    {
        public UnreachableException()
            : base("Branch should be unreachable")
        {
        }
    }
}
