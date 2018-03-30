using System.Collections.Generic;
using System.Linq;

namespace Nett.Parser.Ast
{
    internal sealed class FloatNode : ValueNode
    {
        public FloatNode(Token token)
        {
            this.Value = this.CheckTokenType(TokenType.Float, token);
        }

        public Token Value { get; }

        public override IEnumerable<Node> Children
            => Enumerable.Empty<Node>();
    }
}
