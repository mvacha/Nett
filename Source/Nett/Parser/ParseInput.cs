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
            => this.index >= this.tokens.Count - 1;

        private Token CurrentToken => this.tokens[this.index];

        public IProduction1 Accept(Func<Token, bool> predicate)
        {
            IProduction production = new Production(this);
            return production.Accept(predicate);
        }

        public IProduction1 Expect(Func<Token, bool> predicate)
        {
            IProduction production = new Production(this);
            return production.Expect(predicate);
        }

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

        private SyntaxErrorNode CreateErrorNode()
            => SyntaxErrorNode.Unexpected(this.CurrentToken);

        private sealed class BrokenProduction : IProduction, IProduction1, IProduction2
        {
            private readonly SyntaxErrorNode error;

            public BrokenProduction(SyntaxErrorNode error)
            {
                this.error = error;
            }

            public Node Accept(Func<Token, bool> predicate, Func<Token, Token, Node> createNode)
                => null;

            public Node Expect(Func<Token, bool> predicate, Func<Token, Token, Node> createNode)
                => this.error;

            Node IProduction1.Accept(Func<Token, bool> predicate, Func<Token, Token, Node> createNode)
            {
                throw new NotImplementedException();
            }

            IProduction2 IProduction1.Accept(Func<Token, bool> predicate)
            {
                throw new NotImplementedException();
            }

            Node IProduction1.Expect(Func<Token, bool> predicate, Func<Token, Token, Node> createNode)
            {
                throw new NotImplementedException();
            }

            IProduction2 IProduction1.Expect(Func<Token, bool> predicate)
            {
                throw new NotImplementedException();
            }
        }
    }
}
