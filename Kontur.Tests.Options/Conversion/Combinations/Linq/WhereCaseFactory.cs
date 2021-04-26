using System.Collections.Generic;
using System.Linq;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Conversion.Combinations.Linq
{
    internal delegate bool IsSuitable(int firstValue);

    internal static class WhereCaseFactory
    {
        internal static IEnumerable<TestCaseData> Create(IEnumerable<Option<int>> testCaseArgs, Option<int> result)
        {
            var args = testCaseArgs.ToArray();

            yield return Create(Option<int>.None(), args, _ => false, "Always filter");
            yield return Create(result, args, _ => true, "Always accept");

            yield return Create(
                Option<int>.None(),
                args,
                value => value == 0,
                "Filter incorrect value");

            yield return Create(
                result,
                args,
                value => value == SelectCasesGenerator.InitialValue,
                "Accept correct value");
        }

        private static TestCaseData Create(
            Option<int> result,
            IReadOnlyCollection<Option<int>> args,
            IsSuitable isSuitable,
            string name)
        {
            var @join = string.Join(" ", args);
            return new(args.Cast<object>().Append(isSuitable).ToArray())
            {
                ExpectedResult = result,
                TestName = $"{name}: {@join} to {result}",
            };
        }
    }
}
