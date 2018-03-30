using Nett.Parser.Ast;

namespace Nett.Parser
{
    internal sealed partial class ParseInput
    {
        private sealed class ErrorProduction : IProduction1, IProduction2
        {
            private readonly SyntaxErrorNode error;

            public ErrorProduction(SyntaxErrorNode error)
            {
                this.error = error;
            }
        }
    }
}
