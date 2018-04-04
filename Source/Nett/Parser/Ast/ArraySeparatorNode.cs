using System.Collections.Generic;
using System.Linq;

namespace Nett.Parser.Ast
{
    internal sealed class ArraySeparatorNode : SymbolNode
    {
        public ArraySeparatorNode(Token symbol, Opt<ArrayItemNode> nextItem)
            : base(symbol)
        {
            this.NextItem = nextItem;
        }

        public Opt<ArrayItemNode> NextItem { get; }

        public override IEnumerable<Node> Children
            => base.Children.Concat(EnumerableEx.NonNullItems<Node>(this.NextItem));
    }
}
