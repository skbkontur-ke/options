﻿using Kontur.Options;

namespace Kontur.Tests.Options.Conversion.Linq.Boxed
{
    internal interface IFixtureCase
    {
        public Option<int> GetOption(int value);
    }
}
