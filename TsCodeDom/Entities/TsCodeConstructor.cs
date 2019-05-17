using System;
using TsCodeDom.Constants;

namespace TsCodeDom.Entities
{
    public class TsCodeConstructor : TsCodeMemberMethod
    {
        #region override
        /// <summary>
        /// Get Source
        /// </summary>
        /// <returns></returns>
        protected override string GetSource()
        {
            if (ReturnType != null)
            {
                throw new Exception("ReturnType for TsCodeConstructor defined!");
            }
            return TsDomConstants.TS_CONSTRUCTOR_NAME;
        }
        #endregion
    }
}
