using System.Collections.Generic;
using System.Linq;
using Kontur.Options;

namespace Kontur.Tests.Options.Conversion.Linq
{
    internal static class SelectCasesGenerator
    {
        internal const int InitialValue = 10;

        internal static IEnumerable<SelectCase> Create(int argumentsCount)
        {
            var terms = Enumerable.Range(InitialValue, argumentsCount).ToArray();

            return GetCases(terms, Option<int>.Some(terms.Sum()));
        }

        private static IEnumerable<SelectCase> GetCases(
            IReadOnlyCollection<int> terms,
            Option<int> successResult)
        {
            var boolPermutations = KPermutationOfBool.Create(terms.Count);
            return boolPermutations
                .Select(permutation => permutation
                    .Zip(terms, (success, value) => new TermInfo(success, value)))
                .Select(termInfos => CreateSelectCase(termInfos, successResult));
        }

        private static SelectCase CreateSelectCase(
            IEnumerable<TermInfo> terms,
            Option<int> successResult)
        {
            return terms
                .Aggregate(
                    new SelectCase(Enumerable.Empty<Option<int>>(), successResult),
                    (accumulator, term) =>
                    {
                        var (arg, newResult) = term.Success
                            ? (Option<int>.Some(term.Value), accumulator.Result)
                            : (Option<int>.None(), Option<int>.None());

                        return new SelectCase(accumulator.Args.Append(arg), newResult);
                    });
        }

        private record TermInfo(bool Success, int Value);
    }
}
