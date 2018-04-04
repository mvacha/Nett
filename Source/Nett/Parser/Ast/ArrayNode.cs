using System.Collections.Generic;

namespace Nett.Parser.Ast
{
    internal sealed class ArrayNode : ValueNode
    {
        private ArrayNode(IReq<SymbolNode> lbrac, IReq<SymbolNode> rbrac)
            : this(lbrac, rbrac, AstNode.None<ArrayItemNode>())
        {
        }

        private ArrayNode(IReq<SymbolNode> lbrac, IReq<SymbolNode> rbrac, IOpt<ArrayItemNode> item)
        {
            this.LBrac = lbrac;
            this.RBrac = rbrac;
            this.Item = item;
        }

        public IReq<SymbolNode> LBrac { get; }

        public IReq<SymbolNode> RBrac { get; }

        public IOpt<ArrayItemNode> Item { get; }

        public override IEnumerable<Node> Children
            => NonNullNodesAsEnumerable(this.LBrac, this.Item, this.RBrac);

        public static ArrayNode Empty(Token lbrac, Token rbrac)
            => new ArrayNode(AstNode.Required(new SymbolNode(lbrac)), AstNode.Required(new SymbolNode(rbrac)));

        public static ArrayNode Create(Token lbrac, Token rbrac, IOpt<ArrayItemNode> item)
            => new ArrayNode(new SymbolNode(lbrac).Req(), new SymbolNode(rbrac).Req(), item);

        public override string ToString()
            => "A";
    }
}
