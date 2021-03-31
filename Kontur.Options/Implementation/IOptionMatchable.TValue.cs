using System;

namespace Kontur.Options
{
    public interface IOptionMatchable<out TValue>
    {
        internal TResult Match<TResult>(Func<TResult> onNone, Func<TValue, TResult> onSome);
    }
}