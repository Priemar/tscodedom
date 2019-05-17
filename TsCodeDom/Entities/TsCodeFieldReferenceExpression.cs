using System;
using TsCodeDom.Constants;

namespace TsCodeDom.Entities
{
    public class TsCodeFieldReferenceExpression : TsCodeExpression
    {
        public string FieldName { get; set; }
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
            if (TargetObject == null)
            {
                throw new ArgumentNullException("TargetObject in TsCodeFieldReferenceExpression is null");
            }
            //return source + indent string
            return string.Format(TsDomConstants.ELEMENT_SUB_REFERENCE_FORMAT, TargetObject.GetSource(options,info), FieldName);
        }
        #endregion
    }
}
