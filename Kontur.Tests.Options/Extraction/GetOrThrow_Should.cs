using System;
using FluentAssertions;
using Kontur.Options;
using Kontur.Options.Unsafe;
using NUnit.Framework;

namespace Kontur.Tests.Options.Extraction
{
    [TestFixture]
    internal class GetOrThrow_Should
    {
        [Test]
        public void Throw_InvalidOperationException_On_None()
        {
            var option = Option.None<int>();

            Func<int> func = () => option.GetOrThrow();

            func.Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void Throw_ValueMissingException_On_None()
        {
            var option = Option.None<int>();

            Func<int> func = () => option.GetOrThrow();

            func.Should().Throw<ValueMissingException>();
        }

        [Test]
        public void Do_No_Throw_On_Some()
        {
            var option = Option.Some(5);

            Func<int> func = () => option.GetOrThrow();

            func.Should().NotThrow();
        }

        [Test]
        public void Throw_MyException_On_None()
        {
            var option = Option.None<int>();

            Func<int> func = () => option.GetOrThrow(new MyException());

            func.Should().Throw<MyException>();
        }

        [Test]
        public void Do_No_Throw_Passed_Exception_On_Some()
        {
            var option = Option.Some(5);

            Func<int> func = () => option.GetOrThrow(new MyException());

            func.Should().NotThrow();
        }

        [Test]
        public void Throw_MyException_From_Factory_On_None()
        {
            var option = Option.None<int>();

            Func<int> func = () => option.GetOrThrow(() => new MyException());

            func.Should().Throw<MyException>();
        }

        [Test]
        public void Do_No_Throw_Passed_Exception_From_Factory_On_Some()
        {
            var option = Option.Some(5);

            Func<int> func = () => option.GetOrThrow(() => new MyException());

            func.Should().NotThrow();
        }

        [Test]
        public void Do_No_Create_Exception_On_Some()
        {
            var option = Option.Some(5);

            option.GetOrThrow(AssertDoNotCalled);
        }

        private static Exception AssertDoNotCalled()
        {
            Assert.Fail("Exception should not be created on some");
            return new Exception();
        }

        private class MyException : Exception
        {
        }
    }
}
