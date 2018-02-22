using Nett.Parser.Info;

namespace Nett.Parser.Productions
{
    internal static class KeyValuePairProduction
    {
        public static ParsedKeyValuePair Apply(ITomlRoot root, TokenBuffer tokens)
        {
            var key = KeyProduction.Apply(tokens);

            var assignment = tokens.ExpectAndConsume(TokenType.Assign);
            var assignmentParseInfo = ParseInfo.CreateFromToken(assignment);

            var inlineTableArray = InlineTableArrayProduction.TryApply(root, tokens);
            if (inlineTableArray != null)
            {
                return CreateKeyValuePair(inlineTableArray);
            }

            var inlineTable = InlineTableProduction.TryApply(root, tokens);
            if (inlineTable != null)
            {
                return CreateKeyValuePair(inlineTable.Value);
            }

            var value = ValueProduction.Apply(root, tokens);
            return CreateKeyValuePair(value);

            ParsedKeyValuePair CreateKeyValuePair(TomlObject v) =>
                new ParsedKeyValuePair(new Key(key.Value, new KeyParseInfo(key.Info, assignmentParseInfo)), v);
        }

        public sealed class Key : Parsed<string, KeyParseInfo>
        {
            public Key(string key, KeyParseInfo info)
                : base(key, info)
            {
            }
        }

        public sealed class ParsedKeyValuePair
        {
            public Key Key { get; }

            public TomlObject Value { get; }

            public ParsedKeyValuePair(Key key, TomlObject value)
            {
                this.Key = key;
                this.Value = value;
            }
        }
    }
}
