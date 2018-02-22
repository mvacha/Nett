namespace Nett.Parser.Productions
{
    using System.Collections.Generic;

    internal static class TableKeyProduction
    {
        public static IList<TomlKey> Apply(TokenBuffer tokens)
        {
            List<TomlKey> keyChain = new List<TomlKey>();
            var key = KeyProduction.Apply(tokens);
            keyChain.Add(new TomlKey(key.Value));

            while (tokens.TryExpect(TokenType.Dot))
            {
                tokens.Consume();
                keyChain.Add(new TomlKey(KeyProduction.TryApply(tokens).Value));
            }

            return keyChain;
        }
    }
}
