using System;

namespace Nett
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class TomlIgnoreAttribute : Attribute
    {
    }
}
