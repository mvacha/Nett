using System.Collections.Generic;

namespace Nett.Parser.Ast
{
    internal sealed class SymbolNode : Node
    {
        public SymbolNode(Token token)
            : this(token, new List<Node>())
        {
        }

        public SymbolNode(Token token, Node child)
            : this(token, new List<Node>() { child })
        {
        }

        public SymbolNode(Token token, IEnumerable<Node> children)
        {
            this.Token = token;
            this.Children = children;
        }

        public Token Token { get; }

        public override IEnumerable<Node> Children { get; }

        public override string ToString()
            => $"s -> {this.Token.value}";
    }
}
