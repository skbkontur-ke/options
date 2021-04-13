using System.Diagnostics.Contracts;

namespace Kontur.Options.Holders
{
    internal sealed class NoneHolder<TValue> : Holder<TValue>
    {
        [Pure]
        public override bool TryGet(
#if NETSTANDARD2_0
            out TValue? value)
#else
            [System.Diagnostics.CodeAnalysis.MaybeNullWhen(returnValue: false)]
            out TValue value)
#endif
        {
            value = default;
            return false;
        }
    }
}
