using System;

namespace TsCodeDom.Enumerations
{
    /// <summary>
    /// TypeAttributes
    /// </summary>
    [Flags]
    public enum TsTypeAttributes
    {
        None = -1,
        Private = 1,
        Public = 2,
        Static = 4,
        Readonly = 8,
        Abstract = 128
    }
}
