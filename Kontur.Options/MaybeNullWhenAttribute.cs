#if NETSTANDARD2_0
// ReSharper disable once CheckNamespace
namespace System.Diagnostics.CodeAnalysis
{
    [AttributeUsage(AttributeTargets.Parameter)]
    internal sealed class MaybeNullWhenAttribute : Attribute
    {
        internal MaybeNullWhenAttribute(bool returnValue)
        {
            _ = returnValue;
        }
    }
}
#endif