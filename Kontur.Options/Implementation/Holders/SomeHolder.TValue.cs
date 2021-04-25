using System.Diagnostics.Contracts;

namespace Kontur.Options.Holders
{
    internal sealed class SomeHolder<TValue> : IHolder<TValue>
    {
        private readonly TValue wrappedValue;

        internal SomeHolder(TValue value)
        {
            wrappedValue = value;
        }

        [Pure]
        public bool TryGet(out TValue value)
        {
            value = wrappedValue;
            return true;
        }
    }
}
