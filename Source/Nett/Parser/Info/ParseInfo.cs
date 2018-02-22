namespace Nett.Parser
{
    internal sealed class ParseInfo
    {
        internal static readonly ParseInfo NotAvailable;

        private const int NoLine = -1;
        private const int NoColumn = -1;

        private static readonly string NoWhitespace = string.Empty;

        private readonly int line = NoLine;
        private readonly int column = NoColumn;
        private readonly string whitespace = NoWhitespace;

        static ParseInfo()
        {
            NotAvailable = new ParseInfo(NoLine, NoColumn, NoWhitespace);
        }

        private ParseInfo(int line, int col, string white)
        {
            this.line = line;
            this.column = col;
            this.whitespace = white;
        }

        public string Whitespace => this.whitespace;

        public static ParseInfo CreateFromToken(Token tkn)
        {
            return new ParseInfo(tkn.line, tkn.col, tkn.whitespace);
        }

        public ParseInfo WhitespaceAppended(string white)
            => new ParseInfo(this.line, this.column, this.whitespace + white);
    }
}
