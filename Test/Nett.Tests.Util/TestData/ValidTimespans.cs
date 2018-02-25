using System;
using Xunit;

namespace Nett.Tests.Util.TestData
{
    public sealed class ValidTimespans : TheoryData<string, TimeSpan>
    {
        public ValidTimespans()
        {
            this.Add("1d", TimeSpan.FromDays(1));
            this.Add("1h", TimeSpan.FromHours(1));
            this.Add("1m", TimeSpan.FromMinutes(1));
            this.Add("1s", TimeSpan.FromSeconds(1));
            this.Add("1ms", TimeSpan.FromMilliseconds(1));
            this.Add("1us", TimeSpan.FromTicks(10));
            this.Add("1d1h1m1s1ms", TimeSpan.FromDays(1) + TimeSpan.FromHours(1) + TimeSpan.FromMinutes(1) + TimeSpan.FromSeconds(1) + TimeSpan.FromMilliseconds(1));
            this.Add("1d1m", TimeSpan.FromDays(1) + TimeSpan.FromMinutes(1));
            this.Add("-1s", TimeSpan.FromSeconds(-1));
            this.Add(" 1d", TimeSpan.FromDays(1));
            this.Add(" 1d    ", TimeSpan.FromDays(1));
        }
    }
}
