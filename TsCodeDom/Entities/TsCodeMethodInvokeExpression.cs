using System.Linq;
using TsCodeDom.Constants;

namespace TsCodeDom.Entities
{
    public class TsCodeMethodInvokeExpression : TsCodeExpression
    {
        public TsCodeMethodReferenceExpression Method { get; set; }
        /// <summary>
        /// Parameters
        /// </summary>
        private TsCodeExpressionCollection _parameters = new TsCodeExpressionCollection();
        public TsCodeExpressionCollection Parameters { get { return _parameters; } }

        #region override
        /// <summary>
        /// GetSoruce
        /// </summary>
        /// <param name="options"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        internal override string GetSource(TsGeneratorOptions options, TsWriteInformation info)
        {
            //method source
            var methodSource = Method.GetSource(options, info);
            string parameters = string.Join(TsDomConstants.PARAMETER_SEPERATOR, Parameters.ToList().Select(el => el.GetSource(options, info)).ToList());
            return string.Format(TsDomConstants.TS_MEMBERMETHOD_FORMAT, methodSource, parameters);
        }
        #endregion
    }
}
