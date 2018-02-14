namespace Nett.Parser.Productions
{
    internal static class KeyProduction
    {
        public static TomlKey Apply(TokenBuffer tokens) => ApplyInternal(tokens, required: true);

        public static TomlKey TryApply(TokenBuffer tokens) => ApplyInternal(tokens, required: false);

        private static TomlKey ApplyInternal(TokenBuffer tokens, bool required)
        {
            var tkn = tokens.Peek();

            if (tokens.TryExpect(TokenType.BareKey) || tokens.TryExpect(TokenType.Integer))
            {
                return CreateKey(tokens.Consume(), KeyType.Bare);
            }
            else if (tokens.TryExpect(TokenType.String))
            {
                return CreateKey(tokens.Consume(), KeyType.Basic);
            }
            else if (tokens.TryExpect(TokenType.LiteralString))
            {
                return CreateKey(tokens.Consume(), KeyType.Literal);
            }
            else if (required)
            {
                var t = tokens.Peek();
                if (t.value == "=")
                {
                    throw Parser.CreateParseError(t, "Key is missing.");
                }
                else
                {
                    throw Parser.CreateParseError(t, $"Failed to parse key because unexpected token '{t.value}' was found.");
                }
            }
            else
            {
                return new TomlKey(string.Empty);
            }
        }

        private static TomlKey CreateKey(Token tkn, KeyType type)
        {
            var key = new TomlKey(tkn.value, type)
            {
                ParseInfo = ParsingInfo.CreateFromToken(tkn)
            };

            return key;
        }
    }
}
