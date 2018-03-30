using System;
using Nett.Parser.Ast;

namespace Nett.Parser
{
    internal sealed partial class ParseInput
    {
        private abstract class ProductionBase
        {
            private static readonly UnacceptableProduction CannotAccept = new UnacceptableProduction();

            private readonly ParseInput input;

            protected ProductionBase(ParseInput input)
            {
                this.input = input;
            }

            protected Token Current
                => this.input.CurrentToken;

            protected Token Advance()
                => this.input.Advance();

            protected T Apply<T>(Func<Token, bool> predicate, Func<Token, T> onSuccess, Func<T> onFail)
                => predicate(this.Current)
                    ? onSuccess(this.input, this.Advance())
                    : onFail();

            protected ErrorProduction CreateError()
                => new ErrorProduction(this.input.CreateErrorNode());

            protected UnacceptableProduction DoNotAccept()
                => CannotAccept;
        }

        private sealed class Production : ProductionBase, IProduction, IProduction1, IProduction2, IProduction3
        {
            private Token token1;
            private Token token2;
            private Token token3;

            public Production(ParseInput input)
                : base(input)
            {
            }

            // IProduction
            IProduction1 IProduction.Accept(Func<Token, bool> predicate)
                => this.Apply(
                    predicate,
                    onSuccess: this.ApplyToken1,
                    onFail: this.DoNotAccept);

            IProduction1 IProduction.Expect(Func<Token, bool> predicate)
                => this.Apply(
                    predicate,
                    onSuccess: this.ApplyToken1,
                    onFail: this.CreateError);

            // IProduction1
            Node IProduction1.CreateNode(Func<Token, Node> onSuccess, Func<SyntaxErrorNode> _)
                => onSuccess(this.token1);

            IProduction2 IProduction1.Accept(Func<Token, bool> predicate)
                => this.Apply(
                    predicate,
                    onSuccess: this.ApplyToken2,
                    onFail: this.DoNotAccept);

            IProduction2 IProduction1.Expect(Func<Token, bool> predicate)
                => this.Apply(
                    predicate,
                    onSuccess: this.ApplyToken2,
                    onFail: this.CreateError);

            // IProduction2
            Node IProduction2.CreateNode(Func<Token, Token, Node> onSuccess, Func<SyntaxErrorNode> _)
                => onSuccess(this.token1, this.token2);

            IProduction3 IProduction2.Accept(Func<Token, bool> predicate)
                => this.Apply(
                    predicate,
                    onSuccess: this.ApplyToken3,
                    onFail: this.DoNotAccept);

            IProduction3 IProduction2.Expect(Func<Token, bool> predicate)
                => this.Apply(
                    predicate,
                    onSuccess: this.ApplyToken3,
                    onFail: this.CreateError);

            // IProduction3
            Node IProduction3.CreateNode(Func<Token, Token, Token, Node> onSuccess, Func<SyntaxErrorNode> _)
                => onSuccess(this.token1, this.token2, this.token3);

            private IProduction1 ApplyToken1(Token t)
            {
                this.token1 = t;
                return this;
            }

            private IProduction2 ApplyToken2(Token t)
            {
                this.token2 = t;
                return this;
            }

            private IProduction3 ApplyToken3(Token t)
            {
                this.token3 = t;
                return this;
            }
        }
    }
}
