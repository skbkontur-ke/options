using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Conversion.Linq
{
    [TestFixture]
    internal class Do_Notation_Should
    {
        private static Task<int> GetCurrentIndex() => Task.FromResult(10);

        private static Task<Option<Product>> GetCurrentProduct() => Task.FromResult(Option.Some(new Product("Pizza")));

        private static Task<Option<string>> GetMessage(Guid userId, int index, Product product)
        {
            var result = $"{userId.ToString().Substring(0, 2)}-{index}-{product.Name}";
            return Task.FromResult(Option.Some(result));
        }

        private static Task<Format> GetFormat(int index, string message)
        {
            var prefix = message.Last() + index.ToString(CultureInfo.InvariantCulture);
            var result = new Format(prefix);
            return Task.FromResult(result);
        }

        private static Option<ConvertResult> Convert(string message, Format format)
        {
            var result = format.Prefix + ": " + message;
            return new ConvertResult(result);
        }

        private static TestCaseData Create(Option<Guid> user, Option<string> result)
        {
            return new TestCaseData(user).Returns(result);
        }

        private static readonly TestCaseData[] Cases =
        {
            Create(Option.None(), Option.None()),
            Create(Option.Some(Guid.Empty), Option.Some("a11: 00-11-Pizza")),
            Create(Option.Some(Guid.Parse("e2dc7316-e772-479f-9f6e-f6a776b17ebb")), Option.Some("a11: e2-11-Pizza")),
        };

        [TestCaseSource(nameof(Cases))]
        public async Task<Option<string>> Process_Chain(Option<Guid> user)
        {
            Option<Guid> GetCurrentUserId() => user;

            var task =
                from userId in GetCurrentUserId()
                from index in GetCurrentIndex()
                from product in GetCurrentProduct()
                let nextIndex = index + 1
                from message in GetMessage(userId, nextIndex, product)
                from format in GetFormat(nextIndex, message)
                select Convert(message, format);

            var result = await task.ConfigureAwait(false);

            return result.Map(c => c.Result);
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