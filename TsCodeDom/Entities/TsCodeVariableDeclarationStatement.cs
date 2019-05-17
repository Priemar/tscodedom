using TsCodeDom.Constants;

namespace TsCodeDom.Entities
{
    /// <summary>
    /// CodeVariable declaration statement
    /// </summary>
    public class TsCodeVariableDeclarationStatement : TsCodeStatement
    {
        public TsCodeExpression InitExpression { get; set; }
        public string Name { get; set; }
        public TsCodeTypeReference InitKeyReference { get; set; }
        public TsCodeTypeReference Type { get; set; }

        #region Override
        /// <summary>
        /// Write Soruce
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="options"></param>
        /// <param name="info"></param>
        internal override void WriteSource(System.IO.StreamWriter writer, TsGeneratorOptions options, TsWriteInformation info)
        {
            //type variableName = initexpression
            //add line indent string
            var source = options.GetPreLineIndentString(info.Depth);
            //add type and variable name
            source += InitKeyReference.TsTypeName + TsDomConstants.ATTRIBUTE_SEPEARATOR + Name;
            //if type is set we will add type
            if (Type != null)
            {
                source = string.Format(TsDomConstants.TS_ELEMENT_TYPE_FORMAT, source, Type.TsTypeName);
            }
            //if there is an init expression add init expression
            if (InitExpression != null)
            {
                source = string.Format(TsDomConstants.ASSIGN_FORMAT, source, InitExpression.GetSource(options, info));
            }
            source += TsDomConstants.EXPRESSION_END;            
            //write source
            writer.WriteLine(source);  
        }
        #endregion
    }
}
