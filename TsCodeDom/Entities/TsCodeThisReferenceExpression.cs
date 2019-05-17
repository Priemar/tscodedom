using TsCodeDom.Constants;

namespace TsCodeDom.Entities
{
    public class TsCodeThisReferenceExpression : TsCodeExpression
    {
        #region override
        /// <summary>
        /// GetSource
        /// </summary>
        /// <param name="options"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        internal override string GetSource(TsGeneratorOptions options, TsWriteInformation info)
        {
            return TsDomConstants.THIS_VALUE;
        }
        #endregion
    }
}
