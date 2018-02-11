using FluentAssertions;
using Xunit;

namespace Nett.Tests.Functional
{
    public partial class WriteFormattedTests
    {
        public class ArrayConfig
        {
            public int[] X { get; set; }

            public string Format(string fmt) => string.Format(fmt, this.X[0], this.X[1]);
        }

        private static readonly ArrayConfig SrcArray = new ArrayConfig() { X = new int[] { 1, 2 } };
        private static readonly ArrayConfig NewArray = new ArrayConfig() { X = new int[] { 11, 22 } };

        [Fact]
        public void WriteFormatted_WhenRootArrayGetsWritten_KeepsFormattingSourceWhitespaces()
        {
            // Arrange
            const string fmt = @"
X =   [   {0},
                       {1}

      ]";
            (var source, var expected) = FormatArray(fmt);

            // Act
            var newToml = Toml.WriteFormatted(NewArray, source);

            // Assert
            newToml.Should().Be(expected);
        }

        private static (string src, string expected) FormatArray(string fmt) => (SrcArray.Format(fmt), NewArray.Format(fmt));
    }
}
