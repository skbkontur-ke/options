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

        public Option<TValue> Option { get; }

        public TResult Result { get; }
    }
}
