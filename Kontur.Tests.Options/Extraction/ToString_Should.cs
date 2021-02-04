using System;
using FluentAssertions;
using Kontur.Options;
using NUnit.Framework;

namespace Kontur.Tests.Options.Extraction
{
    [TestFixture]
    internal class ToString_Should
    {
        [Test]
        public void Some()
        {
            var expected = Guid.NewGuid();
            var option = Option.Some(expected);

            var result = option.ToString();

            result.Should().Contain(expected.ToString());
        }

        [Test]
        public void None()
        {
            var option = Option.None<Guid>();

            var result = option.ToString();

            result.Should().ContainAll(nameof(Guid), "None");
        }
    }
}
