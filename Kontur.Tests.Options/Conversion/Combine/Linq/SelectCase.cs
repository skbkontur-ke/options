using System.Collections.Generic;
using System.Linq;
using Kontur.Options;

namespace Kontur.Tests.Options.Conversion.Combine.Linq
{
    internal class SelectCase
    {
        internal SelectCase(IEnumerable<Option<int>> args, Option<int> result)
        {
            Args = args;
            Result = result;
        }

        internal IEnumerable<Option<int>> Args { get; }

        internal Option<int> Result { get; }

        public override bool Equals(object? obj)
        {
            return obj is SelectCase other && Equals(other);
        }

        private bool Equals(SelectCase other)
        {
            return Result.Equals(other.Result) && Args.SequenceEqual(other.Args);
        }

        public override int GetHashCode()
        {
            return (args: Args, Result).GetHashCode();
        }

        public override string ToString()
        {
            var serialized = string.Join("; ", Args.Select(a => $"({a})"));
            return $"Result: {Result}. Args: " + serialized;
        }
    }
}
