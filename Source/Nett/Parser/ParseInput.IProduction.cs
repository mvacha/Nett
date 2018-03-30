using System;
using Nett.Parser.Ast;

namespace Nett.Parser
{
    internal sealed partial class ParseInput
    {
        internal interface IProduction
        {
            IProduction1 Accept(Func<Token, bool> predicate);

            IProduction1 Expect(Func<Token, bool> predicate);
        }

        internal interface IProduction1
        {
            Node CreateNode(Func<Token, Node> onSuccess, Func<SyntaxErrorNode> onError = null);

            IProduction2 Accept(Func<Token, bool> predicate);

            IProduction2 Expect(Func<Token, bool> predicate);
        }

        internal interface IProduction2
        {
            Node CreateNode(Func<Token, Token, Node> onSuccess, Func<SyntaxErrorNode> onError = null);

            IProduction3 Accept(Func<Token, bool> predicate);

            IProduction3 Expect(Func<Token, bool> predicate);
        }

        internal interface IProduction3
        {
            Node CreateNode(Func<Token, Token, Token, Node> onSuccess, Func<SyntaxErrorNode> onError = null);
        }
    }
}
