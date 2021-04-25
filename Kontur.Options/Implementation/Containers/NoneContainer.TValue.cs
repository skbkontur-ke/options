using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace Kontur.Options.Containers
{
    internal sealed class NoneContainer<TValue> : IContainer<TValue>
    {
        private static readonly Lazy<NoneContainer<TValue>> Provider = new(() => new());

        private NoneContainer()
        {
        }

        internal static IContainer<TValue> Instance => Provider.Value;

        [Pure]
        public bool TryGet([MaybeNullWhen(returnValue: false)] out TValue value)
        {
            value = default;
            return false;
        }
    }
}
