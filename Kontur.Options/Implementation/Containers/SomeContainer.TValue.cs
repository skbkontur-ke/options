using System.Diagnostics.Contracts;

namespace Kontur.Options.Containers
{
    internal sealed class SomeContainer<TValue> : IContainer<TValue>
    {
        private readonly TValue storedValue;

        internal SomeContainer(TValue value)
        {
            storedValue = value;
        }

        [Pure]
        public bool TryGet(out TValue value)
        {
            value = storedValue;
            return true;
        }
    }
}
