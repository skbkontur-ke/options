using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace Kontur.Options.Holders
{
    internal sealed class NoneHolder<TValue> : IHolder<TValue>
    {
        private static readonly Lazy<NoneHolder<TValue>> Provider = new(() => new());

        private NoneHolder()
        {
        }

        internal static IHolder<TValue> Instance => Provider.Value;

        [Pure]
        public bool TryGet([MaybeNullWhen(returnValue: false)] out TValue value)
        {
            value = default;
            return false;
        }
    }
}
