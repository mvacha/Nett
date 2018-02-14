namespace Nett
{
    internal enum KeyType
    {
        Undefined,
        Bare,
        Basic,
        Literal,
    }

    internal sealed class KeyMetaInfo
    {
        public KeyMetaInfo(KeyType keyType)
        {
            this.KeyType = keyType;
        }

        public KeyType KeyType { get; } = KeyType.Undefined;

        public ParsingInfo ParseInfo { get; } = null;
    }
}
