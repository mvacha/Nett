using System;
using System.Collections.Generic;
using System.Linq;

namespace Nett.Parser.Ast
{
    internal sealed class TableNode : Node
    {
        public TableNode(Token key)
        {
            if (key.type != TokenType.Key)
            {
                throw new ArgumentException($"Token has to be of type 'key' but is '{key.type}'.");
            }

            this.Key = key;
        }

        public Token Key { get; }

        public override IEnumerable<Node> Children
            => Enumerable.Empty<Node>();
    }
}
