using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace Kontur.Options.Holders
{
    internal interface IHolder<TValue>
    {
        [Pure]
        bool TryGet([MaybeNullWhen(returnValue: false)] out TValue value);
    }
}
