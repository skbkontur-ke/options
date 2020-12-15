using System.Collections.Generic;
using System.Linq;
using Kontur.Extern.Options;

namespace Kontur.Extern.Tests.Options.Conversion.Linq
{
    internal class SelectCase
    {
        private readonly IEnumerable<Option<int>> args;

        internal SelectCase(IEnumerable<Option<int>> args, Option<int> result)
        {
            this.args = args;
            Result = result;
        }

        internal Option<int> Result { get; }

        internal object[] Args => args.Cast<object>().ToArray();

        public override bool Equals(object? obj)
        {
            return obj is SelectCase other && Equals(other);
        }

        private bool Equals(SelectCase other)
        {
            return Result.Equals(other.Result) && args.SequenceEqual(other.args);
        }

        public override int GetHashCode()
        {
            return (args, Result).GetHashCode();
        }

        public override string ToString()
        {
            var serialized = string.Join("; ", args.Select(a => $"({a})"));
            return $"Result: {Result}. Args: " + serialized;
        }
    }
}
