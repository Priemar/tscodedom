using System.Collections.Generic;
using TsCodeDom.Enumerations;

namespace TsCodeDom.Mappings
{
    internal static class TsTypeAttributeMappings
    {
        internal static readonly Dictionary<TsTypeAttributes, string> TypeMappings = new Dictionary<TsTypeAttributes, string>()
        {
            {TsTypeAttributes.Private, "private"},
            {TsTypeAttributes.Public, "public"},
            {TsTypeAttributes.Static, "static"},
            {TsTypeAttributes.Readonly, "readonly"}
        };
    }
}
