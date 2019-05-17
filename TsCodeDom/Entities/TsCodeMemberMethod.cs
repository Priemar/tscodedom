using System;
using System.Linq;
using TsCodeDom.Constants;
using TsCodeDom.Enumerations;
using TsCodeDom.Mappings;

namespace TsCodeDom.Entities
{
    public class TsCodeMemberMethod : TsCodeTypeMember
    {
        #region Properties
        /// <summary>
        /// ReturnType
        /// </summary>
        public TsCodeTypeReference ReturnType { get; set; }
        /// <summary>
        /// Parameters
        /// </summary>
        private readonly TsCodeParameterDeclarationExpressionCollection _parameters = new TsCodeParameterDeclarationExpressionCollection();
        public TsCodeParameterDeclarationExpressionCollection Parameters
        {
            get { return _parameters; }
        }
        /// <summary>
        /// Statements
        /// </summary>
        private readonly TsCodeStatementCollection _statements = new TsCodeStatementCollection();
        public TsCodeStatementCollection Statements
        {
            get { return _statements; }
        }

        #endregion

        #region virtual methods
        /// <summary>
        /// GetSource
        /// </summary>
        /// <returns></returns>
        protected virtual string GetSource()
        {
            return Name;
        }
        #endregion

        #region Override
        /// <summary>
        /// Write Source
        /// attribute methodname() : returntype
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="options"></param>
        /// <param name="info"></param>
        internal override void WriteSource(System.IO.StreamWriter writer, TsGeneratorOptions options, TsWriteInformation info)
        {
            //write base stuff
            base.WriteSource(writer, options, info);
            //check
            if (Decorators.Any())
            {
                throw new NotImplementedException("CodeMemberMethod, CustomAttributes not implemented yet!");
            }            
            //set attribute
            var methodTypeSource = GetSource();
            if (Attributes != TsMemberAttributes.None)
            {
                methodTypeSource = TsMemberAttributeMappings.TypeMappings[Attributes] + TsDomConstants.ATTRIBUTE_SEPEARATOR + methodTypeSource;
            }
            //add method source
            var methodSource = options.GetPreLineIndentString(info.Depth) + string.Format(TsDomConstants.TS_MEMBERMETHOD_FORMAT, methodTypeSource, GetParameterSource(options, info));
            //return type (empty string if not available)            
            if (ReturnType != null)
            {
                methodSource = string.Format(TsDomConstants.TS_ELEMENT_TYPE_FORMAT, methodSource, ReturnType.TsTypeName);
            }
            // we only write method info for interfaces
            if (info.ForType != TsElementTypes.Interface)
            {
                //add statement begin
                methodSource += TsDomConstants.ATTRIBUTE_SEPEARATOR + TsDomConstants.STATEMENT_BRACKET_BEGIN;
                //write begin line
                writer.WriteLine(methodSource);
                //write statemetns
                Statements.ToList().ForEach(el => el.WriteSource(writer, options, info.Clone(info.Depth + 1)));
                //write end source
                writer.WriteLine(GetStatementEnd(options, info.Depth));
            }
            else
            {
                methodSource += TsDomConstants.EXPRESSION_END;
                writer.WriteLine(methodSource);
            }
            
        }
        #endregion

        #region private methods
        /// <summary>
        /// Creates the parametersource
        /// </summary>
        /// <returns></returns>
        private string GetParameterSource(TsGeneratorOptions options, TsWriteInformation info)
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
                            throw new Exception("TsCodeMemberMethod, required parameter cant follow an optional parameter");        
                        }
                    }                    
                }
                return string.Join(TsDomConstants.PARAMETER_SEPERATOR, Parameters.Select(el => el.GetSource(options, info)));
            }
            else
            {
                return string.Empty;
            }
        }
        #endregion
    }
}
