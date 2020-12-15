using System;
using FluentAssertions;
using Kontur.Extern.Options;
using Kontur.Extern.Options.Unsafe;
using NUnit.Framework;

namespace Kontur.Extern.Tests.Options.Extraction
{
    [TestFixture]
    internal class EnsureHasValue_Should
    {
        [Test]
        public void Throw_On_None()
        {
            var option = Option.None<int>();

            Action action = () => option.EnsureHasValue();

            action.Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void Do_No_Throw_On_Some()
        {
            var option = Option.Some(5);

            Action action = () => option.EnsureHasValue();

            action.Should().NotThrow();
        }
    }
}
