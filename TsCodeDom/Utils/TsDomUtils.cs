using TsCodeDom.Constants;
using TsCodeDom.Enumerations;
using TsCodeDom.Mappings;

namespace TsCodeDom.Utils
{
    internal static class TsDomUtils
    {
        /// <summary>
        /// Combine MemberAttributes
        /// </summary>
        /// <param name="attributes"></param>
        /// <returns></returns>
        internal static string CombineTypeAttributeString(TsTypeAttributes attributes, string statement)
        {
            return string.Format(TsDomConstants.TS_ATTRIBUTE_COMBINE_FORMAT, TsTypeAttributeMappings.TypeMappings[attributes], statement);
        }
    }
}
