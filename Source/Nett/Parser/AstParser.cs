using System;
using Nett.Parser.Ast;

namespace Nett.Parser
{
    internal sealed class AstParser
    {
        private readonly ParseInput input;

        public AstParser(ParseInput input)
        {
            this.input = input;
        }
        Node Parse()
        {
            this.Expression();
            this.ExpressionChain();
        }



        void Expression()
        {
            if (this.input.Accept(TokenType.Comment, out var comment)) { }
            else if (this.input.Accept(TokenType.Key)
                && this.input.Expect(TokenType.Assign))
            {
                Value();
            }
            else if (this.input.Accept(TokenType.LBrac, out _)
                && this.input.Expect(TokenType.Key)
                && this.input.Expect(TokenType.RBrac)) { }
            else if ()
        }

        private void ExpressionChain()
        {

        }

        private void Value()
        {
            if (this.input.Accept(TokenType.Float)) { }
            else if (this.input.Accept(TokenType.Integer)) { }
            else if (this.input.Accept(TokenType.LBrac))
            {
                this.ArrayItem();
                this.input.Expect(TokenType.RBrac);
            }
            else if (this.input.Accept(TokenType.LCurly))
            {
                this.InlineTable();
                this.input.Expect(TokenType.RCurly);
            }
        }

        private void ArrayItem()
        {
            if (this.input.Is(TokenType.RBrac) { }
            else
            {
                this.Value();
                this.ArrayCombine();
            }
        }

        private void ArrayCombine()
        {
            if (this.input.Is(TokenType.RBrac)) { this.Epsilon(); }
            else if (this.input.Accept(TokenType.Comma))
            {
                this.NextArrayItem();
            }
            else
            {
                throw new Exception();
            }
        }

        private void NextArrayItem()
        {
            if (this.input.Is(TokenType.RBrac)) { this.Epsilon(); }
            else
            {
                this.ArrayItem();
            }
        }

        private void InlineTable()
        {

        }

        private void Epsilon()
        {
            // Readability method
        }
    }
}
