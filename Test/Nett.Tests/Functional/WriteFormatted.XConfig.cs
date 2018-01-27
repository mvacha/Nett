using FluentAssertions;
using Xunit;

namespace Nett.Tests.Functional
{
    public partial class WriteFormatted
    {
        private static readonly XConfig srcX = new XConfig() { X = 1 };
        private static readonly XConfig newX = new XConfig() { X = 11 };

        private class XConfig
        {
            public int X { get; set; }

            public string Format(string fmt) => string.Format(fmt, this.X);
        }

        [Fact]
        public void WhenThereIsWhitespaceBetweenKeyAndAssignment_OutputHasSameWhitespace()
        {
            // Arrange
            (var fmtSrc, var expected) = FormatX(@"X    = {0}");

            // Act
            var written = Toml.WriteFormatted(newX, fmtSrc);

            // Assert
            written.Should().Be(expected);
        }

        [Fact]
        public void WhenThereIsWhitespaceBetweenAssignmentAndValue_OutputHasSameWhitespace()
        {
            // Arrange
            (var fmtSrc, var expected) = FormatX(@"X =        {0}");

            // Act
            var written = Toml.WriteFormatted(newX, fmtSrc);

            // Assert
            written.Should().Be(expected);
        }

        [Fact]
        public void WhenThereIsWhitesapceInFrontOfAPrependComment_OutputHasTheSameWhitespace()
        {
            // Arrange
            string fmt = @"      # This comment has quite some spaces
X = {0}";
            (var srcFmt, var expected) = FormatX(fmt);

            // Act
            var result = Toml.WriteFormatted(newX, srcFmt);

            // Assert
            result.Should().Be(expected);
        }

        [Fact]
        public void WhenThereIsWhitespaceInFrontOfAppendComment_OutputHasTheSameWhitespace()
        {
            // Arrange
            (var srcFmt, var expected) = FormatX("X = {0}         # This comment has quite some spaces in between");

            // Act
            var result = Toml.WriteFormatted(newX, srcFmt);

            // Assert
            result.Should().Be(expected);
        }

        private static (string src, string expected) FormatX(string fmt)
        {
            return (srcX.Format(fmt), newX.Format(fmt));
        }
    }
}
