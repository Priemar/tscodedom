using TsCodeDom.Constants;

namespace TsCodeDom.Entities
{
    public class TsCodeMethodReferenceExpression : TsCodeExpression
    {
        public string MethodName { get; set; }
        public TsCodeExpression TargetObject { get; set; }

        #region override
        /// <summary>
        /// GetSource
        /// </summary>
        /// <param name="options"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        internal override string GetSource(TsGeneratorOptions options, TsWriteInformation info)
        {
            var source = string.Format(TsDomConstants.ELEMENT_SUB_REFERENCE_FORMAT, TargetObject.GetSource(options, info), MethodName);            
            return source;
        }
        #endregion
    }
}
