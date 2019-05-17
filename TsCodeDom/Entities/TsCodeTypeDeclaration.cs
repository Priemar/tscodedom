using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TsCodeDom.Constants;
using TsCodeDom.Enumerations;

namespace TsCodeDom.Entities
{
    /// <summary>
    /// TsCodeType Declaration
    /// </summary>
    public class TsCodeTypeDeclaration : TsCodeTypeMember
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        public TsCodeTypeDeclaration(string name)
        {
            Name = name;
        }
        #endregion

        #region Properties and Members
        /// <summary>
        /// BaseTypes
        /// </summary>
        private TsCodeTypeReferenceCollection _baseTypes = new TsCodeTypeReferenceCollection();
        public TsCodeTypeReferenceCollection BaseTypes
        {
            get { return _baseTypes; }
        }

        /// <summary>
        /// ElementType
        /// </summary>
        private TsElementTypes _elementType = TsElementTypes.Class;
        public TsElementTypes ElementType
        {
            get { return _elementType; }
            set { _elementType = value; }
        }
        /// <summary>
        /// TypeAttributes default : public
        /// </summary>
        private TsTypeAttributes _attributes = TsTypeAttributes.Public;
        public TsTypeAttributes Attributes
        {
            set { _attributes = value; }
            get { return _attributes; }
        }

        /// <summary>
        /// MemberCollection
        /// </summary>
        private TsCodeTypeMemberCollection _members = new TsCodeTypeMemberCollection();
        public TsCodeTypeMemberCollection Members
        {
            get { return _members; }
        }
        #endregion

        #region override
        /// <summary>
        /// Write Source
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="options"></param>
        /// <param name="depth"></param>
        internal override void WriteSource(StreamWriter writer, TsGeneratorOptions options, TsWriteInformation info)
        {
            //write base stuff
            base.WriteSource(writer, options, info);           
            string detail;
            if (!IsValid(out detail))
            {
                throw new Exception(string.Format("Type({0}) {1}",Name,detail));
            }
            //add custom attributes
            AddCustomAttributes(writer, options, info.Depth);
            //get classname
            string classStatementSource = string.Format(GetTypeFormatString(), Name);
            //if its abstract add abstract keyword
            if ((Attributes & TsTypeAttributes.Abstract) != 0)
            {
                classStatementSource = string.Format(TsDomConstants.TS_ABSTRACT_FORMAT, classStatementSource);
            }
            //if attributes is public
            if ((Attributes & TsTypeAttributes.Public) != 0)
            {
                classStatementSource = string.Format(TsDomConstants.TS_EXPORT_FORMAT, classStatementSource);
            }            
            //add baseTypes
            classStatementSource = AddBaseTypes(classStatementSource);
            //if its a constant (constants assign the code)
            if (ElementType == TsElementTypes.Constant)
            {
                classStatementSource = string.Format(TsDomConstants.ASSIGN_FORMAT, classStatementSource, string.Empty);
            }
            //add begin statement
            classStatementSource = AddStatementBegin(classStatementSource, options, info.Depth);
            //write
            writer.WriteLine(classStatementSource);
            //add content
            AddContent(writer, options, info.Depth + 1);
            //perpare endStatement
            var endStatement = GetStatementEnd(options, info.Depth);
            if (ElementType == TsElementTypes.Constant)
            {
                endStatement += TsDomConstants.EXPRESSION_END;
            }
            //write close statement
            writer.WriteLine(endStatement);
        }
        #endregion

        #region private Methods
        /// <summary>
        /// AddCustomAttributes
        /// </summary>
        /// <param name="writer"></param>
        private void AddCustomAttributes(StreamWriter writer,TsGeneratorOptions options, int depth)
        {
            //check if there are custom attributes
            if (Decorators.Any())
            {
                //add indent
                var customAttributeString = options.GetPreLineIndentString(depth);
                //get source
                var attributeList = Decorators.Select(el => el.GetSource());
                customAttributeString += string.Join(TsDomConstants.ATTRIBUTE_SEPEARATOR, attributeList);
                //write custom attribute string
                writer.WriteLine(customAttributeString);
            }
        }
        /// <summary>
        /// Check if its valid
        /// </summary>
        /// <returns></returns>
        private bool IsValid(out string detail)
        {
            detail = null;
            
            return true;
        }
        /// <summary>
        /// AddBaseTypes
        /// </summary>
        /// <param name="statementSource"></param>
        private string AddBaseTypes(string statementSource)
        {
            //add baseTypes
            if (BaseTypes.Any())
            {
                //check if there are only interfaces and classes in the basetype list
                if (BaseTypes.Any(el => el.ElementType == TsElementTypes.Enumerations))
                {
                    throw new Exception(string.Format("Type ({0}) contains baseTypes which are not interface/class", Name));
                }
                //if this element is a class, we need to add the baseType
                if (ElementType == TsElementTypes.Class)
                {
                    //if there is more than one baseType which is not an interface, throw exeption, bec we can only extend a single class
                    if (BaseTypes.Count(el => el.ElementType == TsElementTypes.Class) > 1)
                    {
                        throw new Exception(string.Format("Type ({0}) can only only extend one baseclass", Name));
                    }
                    //Get baseType
                    var baseType = BaseTypes.FirstOrDefault(el => el.ElementType == TsElementTypes.Class);
                    if (baseType != null)
                    {
                        statementSource = string.Format(TsDomConstants.TS_BASETYPE_EXTENDS_FORMAT, statementSource, baseType.TsTypeName);
                    }
                    //a class can only implement interfaces
                    List<TsCodeTypeReference> implementTypes = BaseTypes.Where(el => el.ElementType == TsElementTypes.Interface).ToList();
                    if (implementTypes.Count > 0)
                    {
                        var typeStringList = implementTypes.Select(el => el.TsTypeName);
                        //combine types
                        var implementsString = string.Join(TsDomConstants.TS_BASETYPE_SEPERATOR, typeStringList);
                        //combine type
                        statementSource = string.Format(TsDomConstants.TS_BASETYPE_IMPLEMENTS_FORMAT, statementSource,
                            implementsString);
                    }
                }
                else if (ElementType == TsElementTypes.Interface)
                {
                    //interface impelments everything including types and baseTypes   
                    List<TsCodeTypeReference> implementTypes = BaseTypes.ToList();
                    if (implementTypes.Count > 0)
                    {
                        var typeStringList = implementTypes.Select(el => el.TsTypeName);
                        //combine types
                        var implementsString = string.Join(TsDomConstants.TS_BASETYPE_SEPERATOR, typeStringList);
                        //combine type
                        statementSource = string.Format(TsDomConstants.TS_BASETYPE_EXTENDS_FORMAT, statementSource,implementsString);
                    }
                }
                //if we are using constants add all interface types
                else if (ElementType == TsElementTypes.Constant)
                {
                    List<TsCodeTypeReference> implementTypes = BaseTypes.ToList();
                    if (implementTypes.Count > 0)
                    {
                        var implementsString = string.Join(TsDomConstants.MULTIPLETYPE_SEPERATOR, implementTypes.Select(el => el.Name));
                        //combine type
                        statementSource = string.Format(TsDomConstants.TS_ELEMENT_TYPE_FORMAT, statementSource, implementsString);
                    }
                }
            }
            return statementSource;
        }
        /// <summary>
        /// AddContent
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="options"></param>
        /// <param name="depth"></param>
        private void AddContent(StreamWriter writer, TsGeneratorOptions options, int depth)
        {            
            //iterate all memebers
            foreach (var member in Members)
            {
                if (member == this)
                {
                    throw new Exception(string.Format("Circular TsCodeTypeDeclarations in ({0})", Name));
                }
                //write source
                member.WriteSource(writer, options, new TsWriteInformation(depth) {ForType = ElementType});
            }
        }
        /// <summary>
        /// GetType format string
        /// </summary>
        /// <returns></returns>
        private string GetTypeFormatString()
        {
            switch (ElementType)
            {
                case TsElementTypes.Enumerations:
                {
                    return TsDomConstants.TS_TYPE_ENUM_FORMAT;
                }
                case TsElementTypes.Interface:
                {
                    return TsDomConstants.TS_TYPE_INTERFACE_FORMAT;
                }
                case TsElementTypes.Class:
                {
                    return TsDomConstants.TS_TYPE_CLASS_FORMAT;
                }
                case TsElementTypes.Constant:
                {
                    return TsDomConstants.TS_TYPE_CONST_FORMAT;
                }    
                default:
                {
                    throw new NotImplementedException(string.Format("CodeTypeDeclaration ElementType ({0}) not implemented", ElementType.ToString()));
                }
            }            
        }
        #endregion
    }
}
