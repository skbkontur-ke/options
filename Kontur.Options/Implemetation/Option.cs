using System.Diagnostics.Contracts;

namespace Kontur.Options
{
    public static class Option
    {
        [Pure]
        public static Option<TValue> Some<TValue>(TValue value)
        {
            return new Some<TValue>(value);
        }

        [Pure]
        public static Option<TValue> None<TValue>()
        {
            return new None<TValue>();
        }

        [Pure]
        public static NoneMarker None()
        {
            return default;
        }
    }
}