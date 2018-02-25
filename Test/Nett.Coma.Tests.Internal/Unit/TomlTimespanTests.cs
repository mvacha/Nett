using System;
using FluentAssertions;
using Nett.Tests.Util.TestData;
using Xunit;

namespace Nett.Coma.Tests.Internal.Unit
{
    public sealed class TomlTimespanTests
    {

        [Theory]
        [ClassData(typeof(ValidTimespans))]
        public void Parse_WithValidTimespanValues_ReutrnsTomlTimespanWithThatValue(string toParse, TimeSpan expected)
        {
            // Act
            bool success = TomlTimeSpan.TryParse(toParse, out TimeSpan parsed);

            // Assert
            success.Should().BeTrue();
            parsed.Should().Be(expected);
        }

        [Theory]
        [ClassData(typeof(InvalidTimespans))]
        public void Parse_WithInvalidTimespanValues_ReutrnsTomlTimespanWithThatValue(string toParse)
        {
            // Act
            bool success = TomlTimeSpan.TryParse(toParse, out TimeSpan parsed);

            // Assert
            success.Should().BeFalse();
        }
    }
}
