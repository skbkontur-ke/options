using System;

namespace Kontur.Options
{
    public interface IOptionMatch<out TValue>
    {
        internal TResult Match<TResult>(Func<TResult> onNone, Func<TValue, TResult> onSome);
    }
}