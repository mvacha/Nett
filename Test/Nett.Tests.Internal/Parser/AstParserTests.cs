using FluentAssertions;
using Nett.Parser;
using Nett.Parser.Ast;
using Xunit;

namespace Nett.Tests.Internal.Parser
{
    public sealed class AstParserTests
    {
        [Fact]
        public void Parse_SingleKeyValueExpression_CorrectAst()
        {
            // Act
            var parsed = Parse("x = 100");

            // Assert
            parsed.PrintTree().Trim().Should().Be(@"
T
 E -> k=V
  k -> x
  s -> =
  V -> 100
".Trim());
        }

        [Fact]
        public void Parse_TableKey_CreatesCorrectAst()
        {
            // Act
            var parsed = Parse("[tablekey]");

            // Assert
            parsed.PrintTree().Trim().Should().Be(@"
T
 T -> tablekey
".Trim());
        }

        [Fact]
        public void Parse_EmptyArrayValue_CreatesCorrectAst()
        {
            // Act
            var parsed = Parse("x = []");

            // Assert
            parsed.PrintTree().Trim().Should().Be(@"
T
 E -> k=V
  k -> x
  s -> =
  A
   s -> [
   s -> ]
".Trim());
        }

        [Fact]
        public void Parse_ArrayValueWithValue_CreatesCorrectAst()
        {
            // Act
            var parsed = Parse("x = [100]");

            // Assert
            parsed.PrintTree().Trim().Should().Be(@"
T
 E -> k=V
  k -> x
  s -> =
  A
   s -> [
   V -> 100
   s -> ]
".Trim());
        }

        [Fact]
        public void Parse_ArrayWithFinalSeparator_CreatesCorrectAst()
        {
            // Act
            var parsed = Parse("x = [100,]");

            // Assert
            parsed.PrintTree().Trim().Should().Be(@"
T
 E -> k=V
  k -> x
  s -> =
  A
   s -> [
   V -> 100
   s -> ,
   s -> ]
".Trim());
        }

        [Fact]
        public void Parse_ArrayWithSecondValue_CreatesCorrectAst()
        {
            // Act
            var parsed = Parse("x = [100,200]");

            // Assert
            parsed.PrintTree().Trim().Should().Be(@"
T
 E -> k=V
  k -> x
  s -> =
  A
   s -> [
   V -> 100
   s -> ,
    V -> 200
   s -> ]
".Trim());
        }

        [Fact]
        public void Parse_SingleKey_CreateSyntaxErrorNode()
        {
            // Act
            var parsed = Parse("x");

            // Assert
            parsed.PrintTree().Trim().Should().Be(@"
T
 X
".Trim());
        }

        [Fact]
        public void Parse_BrokenKeyValueExpression_CreatesAstWithSyntaxErrorNode()
        {
            // Act
            var parsed = Parse("x = ");

            // Assert
            parsed.PrintTree().Trim().Should().Be(@"
T
 E -> k=V
  k -> x
  s -> =
  X
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
