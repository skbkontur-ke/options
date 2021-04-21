using System;
using FluentAssertions;
using Kontur.Options;
using Kontur.Options.Unsafe;
using Kontur.Tests.Options.Extraction.Ensure.None.Override.LibraryNamespace;
using NUnit.Framework;

namespace Kontur.Tests.Options.Extraction.Ensure.None.Override.EndUserNamespace
{
    [TestFixture]
    internal class Method_Should
    {

        [Test]
        public void Throw_If_Some_For_Other_Values_With_Imported_Namespace()
        {
            var option = Option<string>.Some("unused");

            Action action = () => option.EnsureNone();

            action.Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void Throw_If_Some()
        {
            var option = Option<CustomValue>.Some(new());

            Action action = () => option.EnsureNone();

            action.Should()
                .Throw<Exception>()
                .WithMessage(Common.ExceptionMessage);
        }

        [Test]
        public void Do_Not_Throw_If_None()
        {
            var option = Option<CustomValue>.None();

            Action action = () => option.EnsureNone();

            action.Should().NotThrow();
        }
    }
}
