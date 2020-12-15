using System;

namespace Kontur.Extern.Options
{
    public interface IOptionMatch<out TValue>
    {
        internal TResult Match<TResult>(Func<TValue, TResult> onSome, Func<TResult> onNone);
    }
}