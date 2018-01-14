using System.Linq;
using FluentAssertions;
using Nett.Tests.Util;

namespace Nett.Tests.Internal.Functional
{
    public sealed class ParseFormatInfoTests
    {
        private const string FuncParseFmtInfo = "Parse format info";

        [FFact(FuncParseFmtInfo, "When comment is preceded with whitespace, parse info has corresponding whitespace")]
        public void ReadTable_WhenCommentHasPrependedWhitespace_ParseInfoHasSameWhitespace()
        {
            // Arrange
            const string WS = "        ";
            string tml = $"x = 1{WS}# Comment";

            // Act
            var tbl = Toml.ReadString(tml);

            // Assert
            var pi = tbl["x"].Comments.ElementAt(0).ParseInfo;
            pi.Whitespace.Should().Be(WS);
        }
    }
}
