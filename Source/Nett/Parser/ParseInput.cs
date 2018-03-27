using System;
using System.Collections.Generic;

namespace Nett.Parser
{
    internal sealed class ParseInput
    {
        private static readonly Token None = Token.EndOfFile(-1, -1);

        private readonly List<Token> tokens;

        private int index = 0;

        public ParseInput(List<Token> tokens)
        {
            this.tokens = tokens;
        }

        public bool Accept(TokenType token)
            => this.Accept(token, out _);

        public bool Accept(TokenType token, out Token accepted)
        {
            accepted = None;

            if (this.CurrentToken.type == token)
            {
                accepted = this.CurrentToken;
                this.Advance();
                return true;
            }

            return false;
        }

        public bool Expect(TokenType token)
        {

        }

        public bool ExpectNewlines()
        {
            throw new NotImplementedException();
        }

        private Token CurrentToken => this.tokens[this.index];

        private void Advance()
        {
            this.index++;
        }

        internal bool Is(TokenType rBrac)
        {
            throw new NotImplementedException();
        }
    }
}
