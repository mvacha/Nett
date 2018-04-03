namespace Nett.Parser.Ast
{
    internal sealed class NodeSlot<T>
        where T : Node
    {
        public T Node { get; }

        public SyntaxErrorNode Error { get; }

        public Node Slot { get; }

        public NodeSlot(Node n)
        {
            this.Node = n as T;
            this.Error = n as SyntaxErrorNode;
            this.Slot = n;
        }
    }
}
