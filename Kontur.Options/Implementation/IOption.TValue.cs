using System;

namespace Kontur.Options
{
    public interface IOption<out TValue>
    {
        internal TResult Match<TResult>(Func<TResult> onNone, Func<TValue, TResult> onSome);
    }
}