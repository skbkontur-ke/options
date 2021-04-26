using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Conversion.Combine.Linq
{
    [TestFixture]
    internal class Do_Notation_Should
    {
        private static Task<int> GetCurrentIndex() => Task.FromResult(10);

        private static Task<Option<Product>> GetCurrentProduct() => Task.FromResult(Option<Product>.Some(new("Pizza")));

        private static Task<Option<string>> GetMessage(Guid userId, int index, Product product)
        {
            var result = $"{userId.ToString().Substring(0, 2)}-{index}-{product.Name}";
            return Task.FromResult(Option<string>.Some(result));
        }

        private static Task<Format> GetFormat(int index, string message)
        {
            var prefix = message.Last() + index.ToString(CultureInfo.InvariantCulture);
            Format result = new(prefix);
            return Task.FromResult(result);
        }

        private static Option<ConvertResult> Convert(string message, Format format)
        {
            var result = format.Prefix + ": " + message;
            return Option<ConvertResult>.Some(new(result));
        }

        private static TestCaseData Create(Option<Guid> user, Option<string> result)
        {
            return new(user) { ExpectedResult = result };
        }

        private static readonly TestCaseData[] Cases =
        {
            Create(Option<Guid>.None(), Option<string>.None()),
            Create(Option<Guid>.Some(Guid.Empty), Option<string>.None()),
            Create(Option<Guid>.Some(Guid.Parse("00dc7316-e772-479f-9f6e-f6a776b17e00")), Option<string>.Some("a11: 00-11-Pizza")),
            Create(Option<Guid>.Some(Guid.Parse("e2dc7316-e772-479f-9f6e-f6a776b17ebb")), Option<string>.Some("a11: e2-11-Pizza")),
        };

        [TestCaseSource(nameof(Cases))]
        public async Task<Option<string>> Process_Chain(Option<Guid> user)
        {
            Option<Guid> GetCurrentUserId() => user;

            var task =
                from userId in GetCurrentUserId()
                where userId != Guid.Empty
                from index in GetCurrentIndex()
                from product in GetCurrentProduct()
                let nextIndex = index + 1
                where nextIndex > 0
                from message in GetMessage(userId, nextIndex, product)
                from format in GetFormat(nextIndex, message)
                from converted in Convert(message, format)
                select converted.Result;

            return await task.ConfigureAwait(false);
        }

        private class Product
        {
            internal Product(string name)
            {
                Name = name;
            }

            internal string Name { get; }
        }

        private class Format
        {
            internal Format(string prefix)
            {
                Prefix = prefix;
            }

            internal string Prefix { get; }
        }

        public class ConvertResult
        {
            internal ConvertResult(string result)
            {
                Result = result;
            }

            internal string Result { get; }
        }
    }
}