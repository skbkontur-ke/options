using System;
using FluentAssertions;
using Kontur.Extern.Options;
using NUnit.Framework;

namespace Kontur.Extern.Tests.Options.Extraction
{
    [TestFixture]
    internal class ToString_Should
    {
        [Test]
        public void OnSome()
        {
            var expected = Guid.NewGuid();
            var option = Option.Some(expected);

            var result = option.ToString();

            result.Should().Contain(expected.ToString());
        }

        [Test]
        public void OnNone()
        {
            var option = Option.None<Guid>();

            var result = option.ToString();

            result.Should().ContainAll(nameof(Guid), "None");
        }
    }
}
