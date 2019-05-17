using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TsCodeDom.Constants;

namespace TsCodeDom.Entities
{
    /// <summary>
    /// Inline method Type
    /// </summary>
    public class TsCodeTypeInlineMethod : TsCodeBase
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>        
        public TsCodeTypeInlineMethod(TsCodeTypeReference returnType)
        {
            if (returnType == null)
            {
                throw new ArgumentNullException("returnType");
            }
            _returnType = returnType;
        }
        #endregion

        #region Properties
        /// <summary>
        /// ReturnType
        /// </summary>
        private TsCodeTypeReference _returnType = null;
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
                return string.Format(TsDomConstants.TS_PARAMETER_TYPE_METHOD_FORMAT, GetParameterSource(), _returnType.TsTypeName);
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
                            throw new Exception("TsCodeTypeInlineMethod, required parameter cant follow an optional parameter");
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
