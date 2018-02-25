namespace Nett
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Extensions;
    using Nett.Parser.Matchers;

    public sealed class TomlTimeSpan : TomlValue<TimeSpan>
    {
        private static readonly List<string> UnitsOrdered = new List<string>()
        {
            "d", "h", "m", "s", "ms", "us",
        };

        internal TomlTimeSpan(ITomlRoot root, TimeSpan value)
            : base(root, value)
        {
        }

        public override string ReadableTypeName => "timespan";

        public override TomlObjectType TomlType => TomlObjectType.TimeSpan;

        public override void Visit(ITomlObjectVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            if (this.Value.Days > 0) { sb.Append(this.Value.Days).Append("d"); }
            if (this.Value.Hours > 0) { sb.Append(this.Value.Hours).Append("h"); }
            if (this.Value.Minutes > 0) { sb.Append(this.Value.Minutes).Append("m"); }
            if (this.Value.Seconds > 0) { sb.Append(this.Value.Seconds).Append("s"); }
            if (this.Value.Milliseconds > 0) { sb.Append(this.Value.Milliseconds).Append("ms"); }

            if (sb.Length <= 0) { sb.Append("0ms"); }

            return sb.ToString();
        }

        internal static bool TryParse(string s, out TimeSpan parsed)
        {
            parsed = TimeSpan.Zero;

            try
            {
                var tokens = Tokenize(s.Trim()).ToList();

                int unitIndex = 0;

                for (int tokenIndex = tokens[0].Type == Token.TokenType.Sign ? 1 : 0; tokenIndex < tokens.Count; tokenIndex += 2)
                {
                    var valueToken = tokens[tokenIndex];
                    var unitToken = tokens[tokenIndex + 1];
                    if (!IsUnitValid(unitToken, ref unitIndex))
                    {
                        return false;
                    }

                    if (valueToken.Type != Token.TokenType.Num || unitToken.Type != Token.TokenType.Unit)
                    {
                        return false;
                    }

                    parsed += TimeSpanFromTokens(valueToken, unitToken);
                }

                parsed = tokens[0].Type == Token.TokenType.Sign ? -parsed : parsed;
                return tokens.Count > 1;
            }
            catch
            {
                return false;
            }

            bool IsUnitValid(Token unit, ref int index)
            {
                for (int i = index; i < UnitsOrdered.Count; i++)
                {
                    if (UnitsOrdered[i] == unit.Value)
                    {
                        index = i + 1;
                        return true;
                    }
                }

                return false;
            }
        }

        internal TomlTimeSpan TimeSpanWithRoot(ITomlRoot root)
        {
            root.CheckNotNull(nameof(root));

            return new TomlTimeSpan(root, this.Value);
        }

        internal override TomlObject CloneFor(ITomlRoot root) => this.CloneTimespanFor(root);

        internal TomlTimeSpan CloneTimespanFor(ITomlRoot root) => CopyComments(new TomlTimeSpan(root, this.Value), this);

        internal override TomlValue ValueWithRoot(ITomlRoot root) => this.TimeSpanWithRoot(root);

        internal override TomlObject WithRoot(ITomlRoot root) => this.TimeSpanWithRoot(root);

        private static TimeSpan TimeSpanFromTokens(Token value, Token unit)
        {
            switch (unit.Value)
            {
                case "d": return ToTimeSpan(TimeSpan.TicksPerDay);
                case "h": return ToTimeSpan(TimeSpan.TicksPerHour);
                case "m": return ToTimeSpan(TimeSpan.TicksPerMinute);
                case "s": return ToTimeSpan(TimeSpan.TicksPerSecond);
                case "ms": return ToTimeSpan(TimeSpan.TicksPerMillisecond);
                case "us": return ToTimeSpan(TimeSpan.TicksPerMillisecond / 1000);
                default: throw new ArgumentException($"Unknown timespan unit '{unit.Value}'.");
            }

            // Work-around for .Net design issue:
            // https://stackoverflow.com/questions/5450439/timespan-frommilliseconds-strange-implementation
            TimeSpan ToTimeSpan(long ticksPer)
                => TimeSpan.FromTicks((long)(double.Parse(value.Value) * (double)ticksPer));
        }

        private static IEnumerable<Token> Tokenize(string s)
        {
            var sb = new StringBuilder(s);

            if (s[0] == '-')
            {
                Consume();
                yield return Token.Sign();
            }

            while (sb.Length > 0)
            {
                yield return ScanNum();
                yield return ScanUnit();
            }

            Token ScanNum()
            {
                StringBuilder num = new StringBuilder(8);

                while (sb[0].InRange('0', '9')) { num.Append(Consume()); }
                if (sb[0] == '.')
                {
                    num.Append(Consume());
                    if (!sb[0].InRange('0', '9')) { return Token.Unknown(); }
                    while (sb[0].InRange('0', '9')) { num.Append(Consume()); }
                }

                return Token.Num(num.ToString());
            }

            Token ScanUnit()
            {
                if (sb.Length <= 0) { return Token.Unknown(); }

                string u = new string(Consume(), 1);

                if (sb.Length > 0 && !sb[0].InRange('0', '9')) { u += Consume(); }

                switch (u)
                {
                    case "d": return Token.Unit("d");
                    case "h": return Token.Unit("h");
                    case "m": return Token.Unit("m");
                    case "s": return Token.Unit("s");
                    case "ms": return Token.Unit("ms");
                    case "us": return Token.Unit("us");
                    default: return Token.Unknown();
                }
            }

            char Consume()
            {
                char c = sb[0];
                sb.Remove(0, 1);
                return c;
            }
        }

        private class Token
        {
            public Token(TokenType type, string value)
            {
                this.Type = type;
                this.Value = value;
            }

            public enum TokenType
            {
                Sign,
                Num,
                Unit,
                Unknown,
            }

            public string Value { get; }

            public TokenType Type { get; }

            public static Token Sign()
                => new Token(TokenType.Sign, string.Empty);

            public static Token Num(string value)
                => new Token(TokenType.Num, value);

            public static Token Unit(string unit)
                => new Token(TokenType.Unit, unit);

            public static Token Unknown()
                => new Token(TokenType.Unknown, string.Empty);
        }
    }
}
