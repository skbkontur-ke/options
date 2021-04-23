using System;

namespace Kontur.Options
{
    internal sealed class None<TValue> : Option<TValue>
    {
        private static readonly Lazy<None<TValue>> Provider = new(() => new());

        private None()
        {
        }

        internal static Option<TValue> Instance => Provider.Value;

        public override TResult Match<TResult>(Func<TResult> onNone, Func<TValue, TResult> onSome)
        {
            return onNone();
        }
    }
}