using Xunit;

namespace Nett.Tests.Util.TestData
{
    public class InvalidTimespans : TheoryData<string>
    {
        public InvalidTimespans()
        {
            this.Add("s");
            this.Add("ds");
            this.Add("ms");
            this.Add("1d1d");
            this.Add("1h1d");
            this.Add("1d 1");
            this.Add("1d1");
            this.Add("1d 1s");
            this.Add("-");
            this.Add("-1");
            this.Add("");
            this.Add(" ");
            this.Add("");
        }
    }
}
