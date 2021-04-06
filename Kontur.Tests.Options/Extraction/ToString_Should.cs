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
            var option = Option<Guid>.Some(expected);

            var result = option.ToString();

            result.Should().ContainAll(nameof(Guid), "Some", expected.ToString());
        }

        [Test]
        public void None()
        {
            var option = Option<Guid>.None();

            var result = option.ToString();

            result.Should().ContainAll(nameof(Guid), "None");
        }
    }
}
