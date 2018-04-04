using System.Collections.Generic;

namespace Nett.Parser.Ast
{
    internal sealed class TomlNode : Node
    {
        public TomlNode(IEnumerable<ExpressionNode> children)
        {
            this.Children = children;
        }

        public override IEnumerable<Node> Children { get; }

        public static TomlNode Empty() => new TomlNode(new List<ExpressionNode>());

        public override string ToString()
            => "T";
    }
}
