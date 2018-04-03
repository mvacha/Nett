using System;
using Nett.Parser.Ast;

namespace Nett.Parser
{
    internal sealed partial class ParseInput
    {
        private sealed class ErrorProduction : IProduction1, IProduction2, IProduction3
        {
            private readonly SyntaxErrorNode error;

            public ErrorProduction(SyntaxErrorNode error)
            {
                this.error = error;
            }

            IProduction2 IProduction1.Accept(Func<Token, bool> predicate)
                => this;

            IProduction3 IProduction2.Accept(Func<Token, bool> predicate)
                => this;

            Node IProduction1.CreateNode(Func<Token, Node> onSuccess, Func<SyntaxErrorNode> onError)
                => this.error;

            Node IProduction2.CreateNode(Func<Token, Token, Node> onSuccess, Func<SyntaxErrorNode> onError)
                => this.error;

            Node IProduction3.CreateNode(Func<Token, Token, Token, Node> onSuccess, Func<SyntaxErrorNode> onError)
                => this.error;

            IProduction2 IProduction1.Expect(Func<Token, bool> predicate)
                => this;

            IProduction3 IProduction2.Expect(Func<Token, bool> predicate)
                => this;
        }
    }
}
