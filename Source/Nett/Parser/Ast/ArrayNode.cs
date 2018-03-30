using System.Collections.Generic;

namespace Nett.Parser.Ast
{
    internal sealed class ArrayNode : ValueNode
    {
        private ArrayNode(Node lbrac, Node rbrac)
            : this(lbrac, rbrac, null, null)
        {
        }

        private ArrayNode(Node lbrac, Node rbrac, Node value, Node separator)
        {
            this.LBrac = (SymbolNode)lbrac;
            this.RBrac = (SymbolNode)rbrac;
            this.Value = (ValueNode)value;
            this.Separator = (SymbolNode)separator;
        }

        public static ArrayNode Empty(Token lbrac, Token rbrac)
            => new ArrayNode(new SymbolNode(lbrac), new SymbolNode(rbrac));

        public static ArrayNode Create(Token lbrac, Token rbrac, Node value, Node separator)
            => new ArrayNode(new SymbolNode(lbrac), new SymbolNode(rbrac), value, separator);

        public SymbolNode LBrac { get; }

        public SymbolNode RBrac { get; }

        public ValueNode Value { get; }

        public SymbolNode Separator { get; }

        public override IEnumerable<Node> Children
        {
            get
            {
                yield return this.Value;
                yield return this.Separator;
            }
        }
    }
}
