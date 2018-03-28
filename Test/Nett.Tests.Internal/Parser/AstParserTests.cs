using FluentAssertions;
using Nett.Parser;
using Nett.Parser.Ast;
using Xunit;

namespace Nett.Tests.Internal.Parser
{
    public sealed class AstParserTests
    {
        [Fact]
        public void Foox()
        {
            // Act
            var parsed = Parse("x = 100");

            // Assert
            parsed.PrintTree().Trim().Should().Be(@"
T
 E -> KV
  k -> x
  s -> =
  V -> 100
".Trim());
        }

        private static Node Parse(string input)
        {
            var lexer = new Lexer(input);
            var tokens = lexer.Lex();
            var parser = new AstParser(new ParseInput(tokens));
            return parser.Parse();
        }
    }
}
