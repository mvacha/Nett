using System;
using System.Collections.Generic;
using Nett.Parser.Ast;

namespace Nett.Parser
{
    internal sealed class AstParser
    {
        private readonly ParseInput input;

        private readonly IProduction<CommentNode> commentProduction;

        public AstParser(ParseInput input)
        {
            this.input = input;

            this.commentProduction = this.input.Accept(t => t == TokenType.Comment, t => new CommentNode(t));
        }

        public TomlNode Parse()
        {
            this.input.SkipWhitespace();

            if (this.input.Eos) { return TomlNode.Empty(); }

            var expressions = new List<Node>();

            var exp = this.Expression();
            expressions.Add(exp);
            //this.NextExpression();

            return new TomlNode(expressions);
        }

        private Node Expression()
        {
            return Comment() ?? KeyValueExpression() ?? Table();

            Node Comment()
                => this.input
                    .Accept(t => t.type == TokenType.Comment)
                    .CreateNode(t => new CommentNode(t));

            Node KeyValueExpression()
                => this.input
                    .Accept(t => t.type == TokenType.BareKey)
                    .Expect(t => t.type == TokenType.Assign)
                    .CreateNode((k, a) => new KeyValueExpressionNode(k, a, this.Value()));

            Node Table()
                => this.input
                    .Accept(t => t.type == TokenType.LBrac)
                    .Expect(t => t.type == TokenType.BareKey)
                    .Expect(t => t.type == TokenType.RBrac)
                    .CreateNode((_, k, __) => new TableNode(k));
        }

        private IEnumerable<ExpressionNode> NextExpression()
        {
            this.input.ExpectNewlines();

            throw new NotImplementedException();
        }

        private Node Value()
        {
            return IntValueNode()
                ?? FloatValueNode()
                ?? Array()
                ?? InlineTable();

            Node FloatValue() => this.input
                .Accept(t => t.type == TokenType.Float)
                .CreateNode(t => new FloatValueNode(t));

            Node IntValue() => this.input
                .Accept(t => t.type == TokenType.Integer)
                .CreateNode(t => new IntValueNode(t));

            Node Array() => this.input
                .Accept(t => t.type == TokenType.LBrac)
                .CreateNode(_ => this.ArrayItem());

            Node InlineTable() => this.input
                .Accept(t => t.type == TokenType.LCurly)
                .CreateNode(_ => this.InlineTable());
        }

        private Node ArrayItem()
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

        private Node InlineTable()
        {

        }

        private void Epsilon()
        {
            // Readability method
        }
    }
}
