using System.Collections.Generic;

namespace Nett.Parser.Ast
{
    internal sealed class KeyValueExpressionNode : ExpressionNode
    {
        public KeyValueExpressionNode(Token key, Token assignment, IReq<ValueNode> value)
        {
            this.Key = new KeyNode(key).Req();
            this.Assignment = new SymbolNode(assignment).Req();
            this.Value = value;
        }

        public IReq<KeyNode> Key { get; }

        public IReq<SymbolNode> Assignment { get; }

        public IReq<ValueNode> Value { get; }

        public override IEnumerable<Node> Children
            => NodesAsEnumerable(this.Key, this.Assignment, this.Value);

        public override string ToString()
            => $"E -> k=V";
    }
}
