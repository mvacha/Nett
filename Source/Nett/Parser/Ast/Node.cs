using System.Collections.Generic;

namespace Nett.Parser.Ast
{
    internal sealed class Node
    {
        public IEnumerable<Node> Children { get; }

        public IEnumerable<Token> Tokens { get; }

        public Node(Token token, IEnumerable<Node> children)
        {
            this.Children = new List<Token>() { token };
        }

        public Node(IEnumerable<Token> tokens, )
    }
}
