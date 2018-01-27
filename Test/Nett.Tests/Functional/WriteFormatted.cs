using FluentAssertions;
using Nett.Tests.Util;

namespace Nett.Tests.Functional
{
    public partial class WriteFormatted
    {
        private const string FuncWriteMerged = "Write Merged";

        public class WhitespaceConfig
        {
            public int X { get; set; } = 200;

            public int Y { get; set; } = 400;

            public const string Default = @"

    # This is Y
Y = 0



# X follows

   X  =     1



";
        }

        [FFact(FuncWriteMerged, "When merge target contains custom whitespace, it stays intact when new values are saved.")]
        public void XXX()
        {
            // Arrange
            var c = new WhitespaceConfig();
            string expected = WhitespaceConfig.Default
                .Replace("0", $"{c.Y}")
                .Replace("1", $"{c.X}");

            // Act
            var newToml = Toml.WriteFormatted(new WhitespaceConfig(), WhitespaceConfig.Default);

            // Assert
            newToml.Should().Be(expected);
        }
    }
}
