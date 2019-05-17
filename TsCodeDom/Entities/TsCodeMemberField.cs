using System;
using System.CodeDom;
using System.Linq;
using TsCodeDom.Constants;
using TsCodeDom.Enumerations;
using TsCodeDom.Mappings;

namespace TsCodeDom.Entities
{
    /// <summary>
    /// MemberField (Properties / Fields)
    /// </summary>
    public class TsCodeMemberField : TsCodeTypeMember
    {
        /// <summary>
        /// IsNullable
        /// </summary>
        public bool IsNullable { set; get; }
        /// <summary>
        /// Init Expression
        /// </summary>
        public TsCodeExpression InitStatement { set; get; }
        /// <summary>
        /// Types
        /// </summary>
        private TsCodeTypeReferenceCollection _types = new TsCodeTypeReferenceCollection();
        public TsCodeTypeReferenceCollection Types
        {
            get { return _types; }
        }        
        /// <summary>
        /// Type Attributes
        /// </summary>
        private TsTypeAttributes _typeAttributes = TsTypeAttributes.None;
        public TsTypeAttributes TypeAttributes
        {
            get { return _typeAttributes; }
            set { _typeAttributes = value; }
        }

        #region TsWriteCodeBase
        /// <summary>
        /// Write Source
        /// </summary>
        /// <param name="writer"></param>
        internal override void WriteSource(System.IO.StreamWriter writer, TsGeneratorOptions options, TsWriteInformation info)
        {
            //write base stuff
            base.WriteSource(writer, options.Clone(options.IndentString, false), info);
            //get line Intend string
            var intendLineString = options.GetPreLineIndentString(info.Depth);
            //membername (nullable for inline objects not allowed)
            var memberName = IsNullable && info.ForType != TsElementTypes.InlineObject ? string.Format(TsDomConstants.NULLABLE_FORMAT, Name) : Name;
            //prepare source
            var source = string.Empty;
            //we dont need the type for constant
            if (info.ForType == TsElementTypes.Constant ||
                info.ForType == TsElementTypes.InlineObject ||
                info.ForType == TsElementTypes.Enumerations)
            {
                //if its an inline object and membername as string => add string signs
                if (info.ForType == TsElementTypes.InlineObject && info.MemberNameAsString)
                {
                    memberName = string.Format(TsDomConstants.STRING_VALUE_FORMAT, memberName);
                }
                source += memberName;
            }
            else
            {
                source += string.Format(TsDomConstants.TS_ELEMENT_TYPE_FORMAT, memberName, GetTypeSource());   
            }            
            //typeattributes are only interresting in classes
            if (info.ForType == TsElementTypes.Class)
            {
                //get type attributes
                var typeAttributes = TsTypeAttributeMappings.TypeMappings.Where(el => TypeAttributes.HasFlag(el.Key)).OrderBy(el=>el.Key).ToList();
                //combine attriubtes                 
                var typeAttributeSource = string.Join(TsDomConstants.ATTRIBUTE_SEPEARATOR, typeAttributes.Select(el => el.Value));
                //add type attribute
                source = string.Format(TsDomConstants.TS_ATTRIBUTE_COMBINE_FORMAT, typeAttributeSource, source);                
            }    
            //add line intendent string
            source = intendLineString + source;
            //add init statement if its set
            if (InitStatement != null)
            {
                AddInitStatement(ref source, info.ForType, options, info);
            }
            //add end seperator (for enum its different
            var endSeperator = info.ForType == TsElementTypes.Enumerations || info.ForType == TsElementTypes.Constant || info.ForType == TsElementTypes.InlineObject ? TsDomConstants.LIST_ELEMENT_SEPERATOR : TsDomConstants.EXPRESSION_END;
            //add end seperator
            source += endSeperator;
            //write
            writer.WriteLine(source);
        }
        #endregion

        #region private methods
        /// <summary>
        /// Add Init statement
        /// </summary>
        /// <param name="source"></param>
        private void AddInitStatement(ref string source, TsElementTypes elementType, TsGeneratorOptions options, TsWriteInformation info)
        {            
            switch (elementType)
            {
                //constant
                case TsElementTypes.Constant:
                {
                    source = string.Format(TsDomConstants.TS_CONSTANT_SETVALUE_FORMAT, source, InitStatement.GetSource(options, info));
                    break;
                }
                case TsElementTypes.Class:                    
                case TsElementTypes.Enumerations:
                {
                    source = string.Format(TsDomConstants.ASSIGN_FORMAT, source, InitStatement.GetSource(options, info));
                    break;
                }
                case TsElementTypes.InlineObject:
                {
                    source = string.Format(TsDomConstants.TS_INLINEOBJECT_SETVALUE_FORMAT, source, InitStatement.GetSource(options, info));
                    break;
                }
                default:
                {
                    throw new NotImplementedException(string.Format("InitStatement for Type ({0}) is not implemented", elementType.ToString()));
                }
            }
        }
        /// <summary>
        /// GetType Source
        /// </summary>
        /// <returns></returns>
        private string GetTypeSource()
        {
            return string.Join(TsDomConstants.MULTIPLETYPE_SEPERATOR, Types.Select(el => el.TsTypeName));
        }
        #endregion
    }
}
