﻿using System;
using FluentAssertions;
using Kontur.Options;
using Kontur.Options.Unsafe;
using Kontur.Tests.Options.Extraction.GetOrThrow.Override.LibraryNamespace;
using NUnit.Framework;

namespace Kontur.Tests.Options.Extraction.GetOrThrow.Override.EndUserNamespace
{
    [TestFixture]
    internal class Method_Should
    {
        [Test]
        public void Import_Namespace_And_Do_Not_Override_Other_Values()
        {
            var option = Option<string>.None();

            Func<string> action = () => option.GetOrThrow();

            action.Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void Throw_If_None()
        {
            var option = Option<CustomValue>.None();

            Func<CustomValue> action = () => option.GetOrThrow();

            action.Should()
                .Throw<Exception>()
                .WithMessage(Common.ExceptionMessage);
        }

        [Test]
        public void Return_Value_If_Some()
        {
            CustomValue expected = new();
            var option = Option<CustomValue>.Some(expected);

            var result = option.GetOrThrow();

            result.Should().Be(expected);
        }
    }
}
