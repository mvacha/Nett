﻿using System.Collections.Generic;

namespace Nett.Parser.Ast
{
    internal sealed class KeyValueExpressionNode : ExpressionNode
    {
        public KeyValueExpressionNode(Token key, Token assignment, Node value)
        {
            this.Key = new KeyNode(key);
            this.Assignment = new SymbolNode(assignment);
            this.Value = (ValueNode)value;
        }

        public KeyNode Key { get; }

        public SymbolNode Assignment { get; }

        public ValueNode Value { get; }

        public override IEnumerable<Node> Children
            => new Node[]
            {
                this.Key,
                this.Assignment,
                this.Value,
            };

        public override string ToString()
            => $"E -> k=V";
    }
}
