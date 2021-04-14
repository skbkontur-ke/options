using Kontur.Options;

namespace Kontur.Tests.Options
{
    internal class UpcastExample<TValue, TResult>
    {
        internal UpcastExample(Option<TValue> option, TResult result)
        {
            Option = option;
            Result = result;
        }

        internal Option<TValue> Option { get; }

        internal TResult Result { get; }
    }
}
