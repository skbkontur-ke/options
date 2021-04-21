using System.Diagnostics.Contracts;

namespace Kontur.Options.Holders
{
    internal sealed class SomeHolder<TValue> : IHolder<TValue>
    {
        private readonly TValue value;

        internal SomeHolder(TValue value)
        {
            this.value = value;
        }

        // ReSharper disable once ParameterHidesMember
        [Pure]
        public bool TryGet(out TValue value)
        {
            value = this.value;
            return true;
        }
    }
}
