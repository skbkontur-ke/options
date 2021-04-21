using System.Diagnostics.Contracts;

namespace Kontur.Options.Holders
{
    internal interface IHolder<TValue>
    {
        [Pure]
        bool TryGet(
#if NETSTANDARD2_0
            out TValue? value);
#else
            [System.Diagnostics.CodeAnalysis.MaybeNullWhen(returnValue: false)] out TValue value);
#endif
    }
}
