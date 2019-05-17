using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TsCodeDom.Constants;

namespace TsCodeDom.Entities
{
    public class TsCodeTypeInlineReference : TsCodeBase
    {

        #region Properties
        /// <summary>
        /// Parameters
        /// </summary>
        private readonly TsCodeParameterDeclarationExpressionCollection _parameters = new TsCodeParameterDeclarationExpressionCollection();
        public TsCodeParameterDeclarationExpressionCollection Parameters
        {
            get { return _parameters; }
        }
        #endregion

        #region internal properties
        /// <summary>
        /// TsTypeName
        /// </summary>
        internal string TsTypeName
        {
            get
            {
                //return result
                return string.Format(TsDomConstants.TS_PARAMETER_TYPE_INLINE_CLASS_FORMAT, GetParameterSource());
            }
        }
        #endregion 

        #region private methods
        /// <summary>
        /// Creates the parametersource
        /// </summary>
        /// <returns></returns>
        private string GetParameterSource()
        {
            if (Parameters.Any())
            {
                //get first optional parameter
                var firstOptionalParameter = Parameters.FirstOrDefault(el => el.IsNullable);
                if (firstOptionalParameter != null)
                {
                    var optionalIndex = Parameters.ToList().IndexOf(firstOptionalParameter);
                    for (int i = optionalIndex + 1; i < Parameters.Count(); i++)
                    {
                        if (!Parameters[i].IsNullable)
                        {
                            throw new Exception("TsCodeTypeInlineReference, required parameter cant follow an optional parameter");
                        }
                    }
                }
                return string.Join(TsDomConstants.LIST_ELEMENT_SEPERATOR, Parameters.Select(el => el.GetSource(new TsGeneratorOptions(), new TsWriteInformation(0))));
            }
            else
            {
                return string.Empty;
            }
        }
        #endregion
    }
}
