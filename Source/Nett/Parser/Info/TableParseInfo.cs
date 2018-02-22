using System.Collections.Generic;
using Nett.Parser.Info;

namespace Nett.Parser.Meta
{
    internal sealed class TableParseInfo
    {
        private readonly Dictionary<string, RowParseInfo> infos = new Dictionary<string, RowParseInfo>();
    }
}
