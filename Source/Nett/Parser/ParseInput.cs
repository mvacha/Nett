using System;
using System.Collections.Generic;

namespace Nett.Parser
{
    internal sealed class ParseInput
    {
        private static readonly Token NoTokenAvailable = Token.EndOfFile(-1, -1);

        private readonly List<Token> tokens;

        private int index = 0;

        public ParseInput(List<Token> tokens)
        {
            this.tokens = tokens;
        }
        public bool Eos
            => this.index >= this.tokens.Count - 1;

        private Token CurrentToken => this.tokens[this.index];

        public bool Accept(TokenType token)
            => this.Accept(token, out _);

        public bool Accept(TokenType token, out Token accepted)
        {
            accepted = NoTokenAvailable;

            if (this.CurrentToken.type == token)
            {
                accepted = this.CurrentToken;
                this.Advance();
                return true;
            }

            return false;
        }

        public bool Expect(TokenType token)
            => this.Expect(token, out _);

        public bool Expect(TokenType token, out Token expected)
        {
            expected = NoTokenAvailable;

            if (this.CurrentToken.type == token)
            {
                expected = this.CurrentToken;
                return true;
            }

            return false;
        }

        public bool ExpectNewlines()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Token> SkipWhitespace()
        {
            while (this.Expect(TokenType.NewLine))
            {
                yield return this.CurrentToken;
                this.Advance();
            }
        }

        private void Advance()
        {
            this.index++;
        }
    }
}
