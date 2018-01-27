using FluentAssertions;
using Nett.Tests.Util;
using Xunit;

namespace Nett.Tests.Functional
{
    public class PreserveSourceToml
    {
        private const string FuncWriteMerged = "Write Merged";

        public class XYConfig
        {
            [TomlComment("Originally defined comment.")]
            public int X { get; set; } = 2;
        }

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

        [FFact(FuncWriteMerged, "When append comment is written, writes that comment with correct formatting")]
        public void WriteMerged_When()
        {
            // Arrange
            const string tgt = @"x = 1         # This comment has quite some spaces in between";
            var tbl = Toml.Create();
            tbl.Add("x", 2);

            // Act
            var result = Toml.WriteFormatted(tbl, tgt);

            // Assert
            result.Should().Be(@"x = 2         # This comment has quite some spaces in between
");

        }


        [FFact(FuncWriteMerged, "When prepend comment is written, writes that comment with target formatting")]
        public void WriteMerged_WhenPrependCommentIsWritten_WritesThatCommentWithTargetFormatting()
        {
            // Arrange
            const string WS = "        ";
            string tgt = $@"{WS}# This comment has quite some spaces
x = 1";
            var tbl = Toml.Create();
            tbl.Add("x", 2);

            // Act
            var result = Toml.WriteFormatted(tbl, tgt);

            // Assert
            result.Should().Be($@"{WS}# This comment has quite some spaces
x = 2
");

        }

        [FFact(FuncWriteMerged, "When merge target comment was changed, leaves comment intact but updates value")]
        public void WriteMerged_WhenCommentInMergeTargetWasChanged_UpdatesValueButLeavesCommentIntact()
        {
            // Arrange
            const string src = @"
# This comment was changed by a user in the file
X = 1
";
            var c = new XYConfig();
            string expected = src.Replace("= 1", $"= {c.X}");

            // Act
            var newToml = Toml.WriteFormatted(new XYConfig(), src);

            // Assert
            newToml.ShouldBeSemanticallyEquivalentTo(expected);
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

        [Fact]
        public void WriteMerged_WhenFmtSrcRowsHaveMultipleNewlineBetweenThem_NewlyWrittenAlsoHasThem()
        {
            // Arrange
            const string src = @"X = 1


Y = 2
";
            var c = new XYConfig();
            string expected = src.Replace("= 1", $"= {c.X}");

            // Act
            var newToml = Toml.WriteFormatted(new XYConfig(), src);

            // Assert
            newToml.Should().Be(expected);
        }
    }
}
