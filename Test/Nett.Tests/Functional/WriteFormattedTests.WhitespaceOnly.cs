using FluentAssertions;
using Xunit;

namespace Nett.Tests.Functional
{
    public partial class WriteFormattedTests
    {
        [Fact]
        public void WriteFormatted_WhenTargetConsistsOfWhitespaceOnly_SameWhitespaceIsWrittenOnSave()
        {
            // Arrange
            const string SourceToml = @"


";

            // Act
            var newToml = Toml.WriteFormatted(new object(), SourceToml);

            // Assert
            newToml.Should().Be(SourceToml);
        }
    }
}
