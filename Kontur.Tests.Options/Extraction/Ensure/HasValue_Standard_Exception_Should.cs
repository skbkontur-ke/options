﻿using System;
using FluentAssertions;
using Kontur.Options;
using Kontur.Options.Unsafe;
using NUnit.Framework;

namespace Kontur.Tests.Options.Extraction.Ensure
{
    [TestFixture(typeof(InvalidOperationException))]
    [TestFixture(typeof(ValueMissingException))]
    internal class HasValue_Standard_Exception_Should<TException>
        where TException : Exception
    {
        [Test]
        public void Throw_If_None()
        {
            var option = Option<int>.None();

            Action action = () => option.EnsureHasValue();

            action.Should().Throw<TException>();
        }
    }
}
