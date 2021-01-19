using System.Diagnostics.Contracts;

namespace Kontur.Options
{
    public static class Option
    {
        [Pure]
        public static NoneMarker None()
        {
            return default;
        }

        [Pure]
        public static Option<TValue> None<TValue>()
        {
            return Option<TValue>.None();
        }

        [Pure]
        public static Option<TValue> Some<TValue>(TValue value)
        {
            return Option<TValue>.Some(value);
        }
    }
}