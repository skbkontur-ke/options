using Kontur.Options;

namespace Kontur.Tests.Options
{
    internal class UpcastExample<TResult>
    {
        internal UpcastExample(Option<Child> option, TResult result)
        {
            Option = option;
            Result = result;
        }

        internal Option<Child> Option { get; }

        internal TResult Result { get; }
    }
}
