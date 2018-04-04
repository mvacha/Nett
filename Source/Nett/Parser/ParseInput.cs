using System;
using System.Collections.Generic;
using Nett.Parser.Ast;

namespace Nett.Parser
{


    internal sealed partial class ParseInput
    {
        private static readonly Token NoTokenAvailable = Token.EndOfFile(-1, -1);

        private readonly List<Token> tokens;

        private int index = 0;

        public ParseInput(List<Token> tokens)
        {
            this.tokens = tokens;
        }

        public bool Eos
            => this.index > this.tokens.Count - 1;

        private Token CurrentToken =>
            this.index < this.tokens.Count
                ? this.tokens[this.index]
                : NoTokenAvailable;

        public IProduction1 Accept(Func<Token, bool> predicate)
        {
            IProduction production = new Production(this);
            return production.Accept(predicate);
        }

        public SyntaxErrorNode CreateErrorNode()
            => SyntaxErrorNode.Unexpected(this.CurrentToken);

        public IProduction1 Expect(Func<Token, bool> predicate)
        {
            IProduction production = new Production(this);
            return production.Expect(predicate);
        }

        public bool Peek(Func<Token, bool> predicate)
            => predicate(this.CurrentToken);

        public bool ExpectNewlines()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Token> SkipWhitespace()
        {
            while (this.CurrentToken.type == TokenType.NewLine)
            {
                yield return this.CurrentToken;
                this.Advance();
            }
        }

        private Token Advance()
            => this.tokens[this.index++];
    }
}
