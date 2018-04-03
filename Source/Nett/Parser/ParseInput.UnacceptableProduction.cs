using System;
using Nett.Parser.Ast;

namespace Nett.Parser
{
    internal sealed partial class ParseInput
    {
        private class UnacceptableProduction : IProduction1, IProduction2, IProduction3
        {
            public UnacceptableProduction()
            {
            }

            public IProduction3 Accept(Func<Token, bool> predicate)
                => this;

            public Node CreateNode(Func<Token, Token, Node> onSuccess, Func<SyntaxErrorNode> onError = null)
                => null;

            public IProduction3 Expect(Func<Token, bool> predicate)
                => this;

            IProduction2 IProduction1.Accept(Func<Token, bool> predicate)
                => this;

            Node IProduction1.CreateNode(Func<Token, Node> onSuccess, Func<SyntaxErrorNode> onError)
                => null;

            Node IProduction3.CreateNode(Func<Token, Token, Token, Node> onSuccess, Func<SyntaxErrorNode> onError)
                => null;

            IProduction2 IProduction1.Expect(Func<Token, bool> predicate)
                => this;
        }
    }
}
