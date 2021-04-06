using System;
using FluentAssertions;
using Kontur.Options;
using Kontur.Options.Unsafe;
using NUnit.Framework;

namespace Kontur.Tests.Options.Extraction.Ensure
{
    [TestFixture]
    internal class HasValue_Should
    {
        [Test]
        public void Throw_If_None()
        {
            var option = Option<int>.None();

            Action action = () => option.EnsureHasValue();

            action.Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void Do_No_Throw_If_Some()
        {
            var option = Option<int>.Some(5);

            Action action = () => option.EnsureHasValue();

            action.Should().NotThrow();
        }
    }
}
