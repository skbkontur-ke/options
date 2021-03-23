using System.Collections.Generic;
using System.Linq;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Conversion.Linq.Plain.Where
{
    internal delegate bool IsSuitable(int firstValue);

    internal static class SelectCaseToNoneTestCasesExtensions
    {
        internal static IEnumerable<TestCaseData> ToTestCases(this IEnumerable<SelectCase> cases)
        {
            return cases.SelectMany(CreateCases);
        }

        private static IEnumerable<TestCaseData> CreateCases(SelectCase testCase)
        {
            var args = testCase.Args.ToArray();

            yield return CreateCase(Option<int>.None(), args, _ => false, "Always filter");
            yield return CreateCase(testCase.Result, args, _ => true, "Always accept");

            yield return CreateCase(
                Option<int>.None(),
                args,
                value => value == 0,
                "Filter incorrect value");

            yield return CreateCase(
                testCase.Result,
                args,
                value => value == SelectCasesGenerator.InitialValue,
                "Accept correct value");
        }

        private static TestCaseData CreateCase(
            Option<int> result,
            IReadOnlyCollection<Option<int>> args,
            IsSuitable isSuitable,
            string name)
        {
            var @join = string.Join(" ", args);
            return new TestCaseData(args.Cast<object>().Append(isSuitable).ToArray())
                .Returns(result)
                .SetName($"{name}: {@join} to {result}");
        }
    }
}
