using System;
using System.Collections.Generic;
using System.Linq;
using Nett.Extensions;

namespace Nett
{
    internal delegate void KeyActionDelegate(TomlKey x, TomlKey y);

    internal static class TableWalker
    {
        private static readonly IEnumerable<string> Empty = new List<string>();

        public static void Walk(TomlTable x, TomlTable y, Action<TomlObject, TomlObject> action, KeyActionDelegate keyAction)
        {
            x.CheckNotNull(nameof(x));
            y.CheckNotNull(nameof(y));
            action.CheckNotNull(nameof(action));

            WalkInternal(x, y, action, keyAction);
        }

        private static void WalkInternal(
            TomlTable x, TomlTable y, Action<TomlObject, TomlObject> action, KeyActionDelegate keyAction)
        {
            var xk = x?.Keys ?? Empty;
            var yk = y?.Keys ?? Empty;
            var ak = xk.Union(yk);

            foreach (var k in ak)
            {
                x.TryGetValue(k, out var xo);
                y.TryGetValue(k, out var yo);

                action(xo, y);

                if (xo.TomlType == TomlObjectType.Table && yo.TomlType == TomlObjectType.Table)
                {
                    WalkInternal((TomlTable)xo, (TomlTable)yo, action, null);
                }
            }
        }
    }
}
