using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace Kontur.Options.Containers
{
    internal interface IContainer<TValue>
    {
        [Pure]
        bool TryGet([MaybeNullWhen(false)] out TValue value);
    }
}
