using System;

namespace Kontur.Options
{
    public interface IMatchable<out TValue>
    {
        internal TResult Match<TResult>(Func<TResult> onNone, Func<TValue, TResult> onSome);
    }
}