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
            return IntValue()
                ?? FloatValue()
                ?? Array()
                ?? InlineTable()
                ?? this.input.CreateErrorNode();

            Node FloatValue()
                => this.input
                    .Accept(t => t.type == TokenType.Float)
                    .CreateNode(t => new FloatValueNode(t));

            Node IntValue()
                => this.input
                    .Accept(t => t.type == TokenType.Integer)
                    .CreateNode(t => new IntValueNode(t));

            Node Array()
                => this.input
                    .Accept(t => t.type == TokenType.LBrac)
                    .CreateNode(t => this.Array(t));

            Node InlineTable()
                => this.input
                    .Accept(t => t.type == TokenType.LCurly)
                    .CreateNode(_ => this.InlineTable());
        }

        private Node Array(Token lbrac)
        {
            var eps = Epsilon();
            if (eps != null) { return eps; }

            Node value = this.Value();
            Node separator = this.ArraySeparator();

            return this.input.Expect(t => t.type == TokenType.RBrac)
                .CreateNode(t => ArrayNode.Create(lbrac, t, value, separator));

            Node Epsilon()
                => this.input
                    .Accept(t => t.type == TokenType.RBrac)
                    .CreateNode(t => ArrayNode.Empty(lbrac, t));
        }

        private Node ArraySeparator()
        {
            if (Epsilon()) { return null; }

            return this.input.Expect(t => t.type == TokenType.Comma)
                .CreateNode(t => new SymbolNode(t, this.Value()));

            bool Epsilon()
                => this.input
                    .Accept(t => t.type == TokenType.RBrac)
                    .CreateNode(t => new SymbolNode(t)) != null;
        }

        private Node InlineTable()
        {
            throw new NotImplementedException();
        }

        private void Epsilon()
        {
            // Readability method
        }
    }
}
