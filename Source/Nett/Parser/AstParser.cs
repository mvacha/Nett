using System;
using System.Collections.Generic;
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

        public TomlNode Parse()
        {
            this.input.SkipWhitespace();

            if (this.input.Eos) { return TomlNode.Empty(); }

            var expressions = new List<ExpressionNode>();

            var exp = this.Expression();
            expressions.Add(exp);
            //this.NextExpression();

            return new TomlNode(expressions);
        }

        private ExpressionNode Expression()
        {
            if (this.input.Accept(TokenType.Comment, out var comment)) { }
            else if (this.input.Accept(TokenType.BareKey, out var key)
                && this.input.Accept(TokenType.Assign, out var assign))
            {
                return new KeyValueExpressionNode(key, assign, this.Value());
            }
            else if (this.input.Accept(TokenType.LBrac, out _)
                && this.input.Expect(TokenType.Key)
                && this.input.Expect(TokenType.RBrac)) { }


            throw new NotImplementedException();
        }

        private IEnumerable<ExpressionNode> NextExpression()
        {
            this.input.ExpectNewlines();

            throw new NotImplementedException();
        }

        private ValueNode Value()
        {
            if (this.input.Accept(TokenType.Float, out var floatToken)) { }
            else if (this.input.Accept(TokenType.Integer, out var intToken)) { return new IntValueNode(intToken); }
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

            throw new NotImplementedException();
        }

        private void ArrayItem()
        {
            if (this.input.Expect(TokenType.RBrac)) { }
            else
            {
                this.Value();
                this.ArrayCombine();
            }
        }

        private void ArrayCombine()
        {
            if (this.input.Expect(TokenType.RBrac)) { this.Epsilon(); }
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
            if (this.input.Expect(TokenType.RBrac)) { this.Epsilon(); }
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
