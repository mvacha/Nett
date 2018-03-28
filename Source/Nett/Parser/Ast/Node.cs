using System.Collections.Generic;
using System.Text;
using Nett.Collections;

namespace Nett.Parser.Ast
{
    internal abstract class Node : IGetChildren<Node>
    {
        public abstract IEnumerable<Node> Children { get; }

        IEnumerable<Node> IGetChildren<Node>.GetChildren()
            => this.Children;

        public string PrintTree()
        {
            var builder = new StringBuilder();
            var allNodes = this.TraversePreOrderWithDepth();

            foreach (var n in allNodes)
            {
                builder.Append(new string(' ', n.Depth))
                    .AppendLine(n.Node.ToString());
            }

            return builder.ToString();
        }
    }
}
