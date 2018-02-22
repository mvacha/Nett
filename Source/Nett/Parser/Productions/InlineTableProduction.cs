using System.Collections.Generic;

namespace Nett.Parser.Productions
{
    internal static class InlineTableProduction
    {
        public static Table Apply(ITomlRoot root, TokenBuffer tokens)
        {
            TomlTable inlineTable = new TomlTable(root, TomlTable.TableTypes.Inline);
            TableParseInfo info = new TableParseInfo();

            tokens.ExpectAndConsume(TokenType.LCurly);

            if (!tokens.TryExpect(TokenType.RBrac))
            {
                var kvp = KeyValuePairProduction.Apply(root, tokens);
                InsertKeyValuePair(kvp);

                while (tokens.TryExpect(TokenType.Comma))
                {
                    tokens.Consume();
                    kvp = KeyValuePairProduction.Apply(root, tokens);
                    InsertKeyValuePair(kvp);
                }
            }

            tokens.ExpectAndConsume(TokenType.RCurly);
            return new Table(inlineTable, info);

            void InsertKeyValuePair(KeyValuePairProduction.ParsedKeyValuePair pair)
            {
                inlineTable.AddRow(pair.Key.Value, pair.Value);
                info.Add(pair.Key);
            }
        }

        public static Table TryApply(ITomlRoot root, TokenBuffer tokens)
            => tokens.TryExpect(TokenType.LCurly)
                ? Apply(root, tokens)
                : null;

        public sealed class TableParseInfo
        {
            private readonly Dictionary<string, KeyValuePairProduction.KeyParseInfo> infos
                = new Dictionary<string, KeyValuePairProduction.KeyParseInfo>();

            public TableParseInfo()
            {
            }

            public void Add(KeyValuePairProduction.Key key)
            {
                this.infos.Add(key.Value, key.Info);
            }
        }

        public sealed class Table : Parsed<TomlTable, TableParseInfo>
        {
            public Table(TomlTable table, TableParseInfo info)
                : base(table, info)
            {
            }
        }
    }
}
