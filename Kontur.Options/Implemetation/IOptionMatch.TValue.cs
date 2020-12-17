using System;

namespace Kontur.Options
{
    public interface IOptionMatch<out TValue>
    {
        internal TResult Match<TResult>(Func<TValue, TResult> onSome, Func<TResult> onNone);
    }
}