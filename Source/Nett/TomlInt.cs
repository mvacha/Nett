using Nett.Extensions;
using Nett.Parser;

namespace Nett
{
    public sealed class TomlInt : TomlValue<long>
    {
        internal TomlInt(ITomlRoot root, long value)
            : base(root, value)
        {
        }

        public override string ReadableTypeName => "int";

        public override TomlObjectType TomlType => TomlObjectType.Int;

        public override void Visit(ITomlObjectVisitor visitor)
        {
            visitor.Visit(this);
        }

        internal static TomlInt FromToken(ITomlRoot root, Token token)
        {
            long value = long.Parse(token.value.Replace("_", string.Empty));
            return new TomlInt(root, value)
            {
                ParseInfo = ParsingInfo.CreateFromToken(token)
            };
        }

        internal override TomlObject CloneFor(ITomlRoot root) => this.CloneIntFor(root);

        internal TomlInt CloneIntFor(ITomlRoot root) => CopyComments(new TomlInt(root, this.Value), this);

        internal override TomlValue ValueWithRoot(ITomlRoot root) => this.IntWithRoot(root);

        internal override TomlObject WithRoot(ITomlRoot root) => this.IntWithRoot(root);

        internal TomlInt IntWithRoot(ITomlRoot root)
        {
            root.CheckNotNull(nameof(root));

            return new TomlInt(root, this.Value);
        }
    }
}
