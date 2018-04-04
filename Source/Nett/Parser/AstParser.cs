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

            var exp = this.Expression().Node;
            expressions.Add(exp);
            //this.NextExpression();

            return new TomlNode(expressions);
        }

        private IReq<ExpressionNode> Expression()
        {
            return Comment().As<ExpressionNode>()
                .Or(KeyValueExpression())
                .Or(Table())
                .Or(this.input.CreateErrorNode());

            IOpt<CommentNode> Comment()
                => this.input
                    .Accept(t => t.type == TokenType.Comment)
                    .CreateNode(t => new CommentNode(t).Opt());

            IOpt<KeyValueExpressionNode> KeyValueExpression()
                => this.input
                    .Accept(t => t.type == TokenType.BareKey)
                    .Expect(t => t.type == TokenType.Assign)
                    .CreateNode((k, a) => new KeyValueExpressionNode(k, a, this.Value()).Opt());

            IOpt<TableNode> Table()
                => this.input
                    .Accept(t => t.type == TokenType.LBrac)
                    .Expect(t => t.type == TokenType.BareKey)
                    .Expect(t => t.type == TokenType.RBrac)
                    .CreateNode((_, k, __) => new TableNode(k).Opt());
        }

        private IEnumerable<ExpressionNode> NextExpression()
        {
            this.input.ExpectNewlines();

            throw new NotImplementedException();
        }

        private IReq<ValueNode> Value()
        {
            return IntValue().As<ValueNode>()
                .Or(FloatValue())
                .Or(Array())
                .Or(InlineTable())
                .Or(this.input.CreateErrorNode());

            IOpt<FloatValueNode> FloatValue()
                => this.input
                    .Accept(t => t.type == TokenType.Float)
                    .CreateNode(t => new FloatValueNode(t).Opt());

            IOpt<IntValueNode> IntValue()
                => this.input
                    .Accept(t => t.type == TokenType.Integer)
                    .CreateNode(t => new IntValueNode(t).Opt());

            IOpt<ArrayNode> Array()
                => this.input
                    .Accept(t => t.type == TokenType.LBrac)
                    .CreateNode(t => this.Array(t).AsOpt());

            IOpt<InlineTableNode> InlineTable()
                => this.input
                    .Accept(t => t.type == TokenType.LCurly)
                    .CreateNode(_ => this.InlineTable().AsOpt());
        }

        private IReq<ArrayNode> Array(Token lbrac)
        {
            var items = this.ArrayItems();
            return this.input.Expect(t => t.type == TokenType.RBrac)
                .CreateNode(t => ArrayNode.Create(lbrac, t, items).Req());
        }

        private IOpt<ArrayItemNode> ArrayItems()
        {
            if (Epsilon()) { return Opt<ArrayItemNode>.None; }

            var value = this.Value();
            var sep = this.ArraySeparator();

            return new ArrayItemNode(value, sep).Opt();

            bool Epsilon()
                => this.input.Peek(t => t.type == TokenType.RBrac);
        }

        private Opt<ArrayItemNode> ArrayItem()
        {
            throw new NotImplementedException();
        }

        private IOpt<ArraySeparatorNode> ArraySeparator()
        {
            if (Epsilon()) { return Opt<ArraySeparatorNode>.None; }

            return this.input.Expect(t => t.type == TokenType.Comma)
                .CreateNode(t => new ArraySeparatorNode(t, this.ArrayItem()).Opt());

            bool Epsilon()
                => this.input.Peek(t => t.type == TokenType.RBrac);
        }

        private IReq<InlineTableNode> InlineTable()
        {
            throw new NotImplementedException();
        }

        private void Epsilon()
        {
            // Readability method
        }
    }
}
