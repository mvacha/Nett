using FluentAssertions;
using Xunit;

namespace Nett.Tests.Functional
{
    public partial class WriteFormattedTests
    {
        private const string FuncWriteMerged = "Write Merged";

        public class WhitespaceConfig
        {
            public int X { get; set; } = 10;

            public int Y { get; set; } = 20;

            public int Z { get; set; } = 30;

            public int[] W { get; set; } = new int[] { 40, 40, 40 };

            public const string Default = @"

Z = 3 # But the user moved Z to the first position

    # This is Y
Y = 2



# X follows

   X  =     1

      W      = [
         4,
             4,
                 4,



      ]

";
        }

        [Fact]
        public void WriteMerged_WhenMergeTargetContainsCustomWhitespace_WhitespaceIsKeptOnSave()
        {
            // Arrange
            var c = new WhitespaceConfig();
            string expected = WhitespaceConfig.Default
                .Replace("1", $"{c.X}")
                .Replace("2", $"{c.Y}")
                .Replace("3", $"{c.Z}")
                .Replace("4", $"{c.W[0]}");

            // Act
            var newToml = Toml.WriteFormatted(new WhitespaceConfig(), WhitespaceConfig.Default);

            // Assert
            newToml.Should().Be(expected);
        }
    }
}
