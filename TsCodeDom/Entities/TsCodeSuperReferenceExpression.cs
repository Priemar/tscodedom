using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TsCodeDom.Constants;

namespace TsCodeDom.Entities
{
    public class TsCodeSuperReferenceExpression : TsCodeExpression
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
            return TsDomConstants.SUPER_VALUE;
        }
        #endregion
    }
}
