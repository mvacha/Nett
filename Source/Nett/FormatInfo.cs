using Nett.Parser;

namespace Nett
{
    internal sealed class ParsingInfo
    {
        internal static readonly ParsingInfo NotAvailable;

        private const int NoLine = -1;
        private const int NoColumn = -1;

        private static readonly string NoWhitespace = string.Empty;

        private readonly int line = NoLine;
        private readonly int column = NoColumn;
        private readonly string whitespace = NoWhitespace;

        static ParsingInfo()
        {
            NotAvailable = new ParsingInfo(NoLine, NoColumn, NoWhitespace);
        }

        private ParsingInfo(int line, int col, string white)
        {
            this.line = line;
            this.column = col;
            this.whitespace = white;
        }

        public string Whitespace => this.whitespace;

        public static ParsingInfo CreateFromToken(Token tkn)
        {
            return new ParsingInfo(tkn.line, tkn.col, tkn.whitespace);
        }

        public ParsingInfo WhitespaceAppended(string white)
            => new ParsingInfo(this.line, this.column, this.whitespace + white);
    }
}
