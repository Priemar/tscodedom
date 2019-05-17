using System.Collections.Generic;
using TsCodeDom.Enumerations;

namespace TsCodeDom.Mappings
{
    internal static class TsMemberAttributeMappings
    {
        internal static readonly Dictionary<TsMemberAttributes, string> TypeMappings = new Dictionary<TsMemberAttributes, string>()
        {
            {TsMemberAttributes.Private, "private"},
            {TsMemberAttributes.Public, "public"},
            {TsMemberAttributes.Abstract, "abstract"},
            {TsMemberAttributes.Protected, "protected"}
        };
    }
}
