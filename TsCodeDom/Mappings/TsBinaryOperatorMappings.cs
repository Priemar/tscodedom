using System.Collections.Generic;
using TsCodeDom.Enumerations;

namespace TsCodeDom.Mappings
{
    internal static class TsBinaryOperatorMappings
    {
        internal static readonly Dictionary<TsCodeBinaryOperatorTypes, string> BinaryOperatorMappings = new Dictionary<TsCodeBinaryOperatorTypes, string>()
        {
            {TsCodeBinaryOperatorTypes.BooleanAnd, "&&"},
            {TsCodeBinaryOperatorTypes.BooleanOr, "||"},
            {TsCodeBinaryOperatorTypes.IdentityEquality, "=="},
            {TsCodeBinaryOperatorTypes.IdentityInequality, "!="}
        };
    }
}
