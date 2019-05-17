using TsCodeDom.Constants;
using TsCodeDom.Enumerations;
using TsCodeDom.Mappings;

namespace TsCodeDom.Entities
{
    public class TsCodeBinaryOperatorExpression : TsCodeExpression
    {
        #region Properties and Members
        public TsCodeExpression Left { get; set; }
        public TsCodeBinaryOperatorTypes Operator { get; set; }
        public TsCodeExpression Right { get; set; }
        #endregion

        #region override
        /// <summary>
        /// GetSource
        /// </summary>
        /// <param name="options"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        internal override string GetSource(TsGeneratorOptions options, TsWriteInformation info)
        {
            //get operator source
            var operatorSource = TsBinaryOperatorMappings.BinaryOperatorMappings[Operator];
            var leftSource = Left.GetSource(options, info);
            var rightSource = Right.GetSource(options, info);
            return string.Format(TsDomConstants.TS_BINARYOPERATOR_EXPRESSION_FORMAT, leftSource, operatorSource, rightSource);
        }
        #endregion
    }
}
