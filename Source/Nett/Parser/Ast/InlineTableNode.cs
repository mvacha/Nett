using System.Collections.Generic;

namespace Nett.Parser.Ast
{
    internal sealed class InlineTableNode : ValueNode
    {
        public override IEnumerable<Node> Children => throw new System.NotImplementedException();
    }
}
