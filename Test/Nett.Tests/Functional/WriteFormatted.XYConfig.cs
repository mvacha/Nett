using FluentAssertions;
using Xunit;

namespace Nett.Tests.Functional
{
    public partial class WriteFormatted
    {
        private static readonly XYConfig srcXY = new XYConfig() { X = 1, Y = 2 };
        private static readonly XYConfig newXY = new XYConfig() { X = 11, Y = 22 };

        public class XYConfig
        {
            public int X { get; set; } = 333;

            public int Y { get; set; } = 666;

            public string Format(string fmt) => string.Format(fmt, this.X, this.Y);
        }

        [Fact]
        public void WriteMerged_WhenFmtSrcRowsHaveMultipleNewlineBetweenThem_NewlyWrittenAlsoHasThem()
        {
            // Arrange
            const string fmt = @"X = {0}


Y = {1}";
            (var srcFmt, var expected) = FormatXY(fmt);

            // Act
            var newToml = Toml.WriteFormatted(newXY, srcFmt);

            // Assert
            newToml.Should().Be(expected);
        }

        private static (string src, string expected) FormatXY(string fmt)
        {
            return (srcXY.Format(fmt), newXY.Format(fmt));
        }
    }
}
