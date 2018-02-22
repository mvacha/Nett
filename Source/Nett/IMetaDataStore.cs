using System.Collections.Generic;

namespace Nett
{
    public interface ITomlRoot
    {
        TomlSettings Settings { get; }
    }
}
