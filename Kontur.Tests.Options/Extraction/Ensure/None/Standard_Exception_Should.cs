using System;
using FluentAssertions;
using Kontur.Options;
using Kontur.Options.Unsafe;
using NUnit.Framework;

namespace Kontur.Tests.Options.Extraction.Ensure.None
{
    [TestFixture(typeof(InvalidOperationException))]
    [TestFixture(typeof(ValueExistsException))]
    internal class Standard_Exception_Should<TException>
        where TException : Exception
    {
        [Test]
        public void Throw_If_Some()
        {
            const string expected = "value";
            var option = Option<string>.Some(expected);

            Action action = () => option.EnsureNone();

            action.Should()
                .Throw<TException>()
                .WithMessage($"*{expected}*");
        }
    }
}
