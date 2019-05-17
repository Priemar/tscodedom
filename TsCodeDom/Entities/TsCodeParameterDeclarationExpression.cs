using System;
using System.Linq;
using TsCodeDom.Constants;
using TsCodeDom.Enumerations;
using TsCodeDom.Mappings;

namespace TsCodeDom.Entities
{
    public class TsCodeParameterDeclarationExpression : TsCodeExpression
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public TsCodeParameterDeclarationExpression()
        {
            
        }
        /// <summary>
        /// Constructor for index parameter definition
        /// </summary>
        /// <param name="isIndexParameter"></param>
        /// <param name="indexName"></param>
        /// <param name="isIndexTypeString">the index type can only be string or number</param>
        public TsCodeParameterDeclarationExpression(string name) : this(null, false, false)
        {
            Name = name;
        }
        public TsCodeParameterDeclarationExpression(string indexName, bool isIndexTypeString) : this(indexName, isIndexTypeString, true)
        {
        }
        private TsCodeParameterDeclarationExpression(string indexName, bool isIndexTypeString, bool isIndexParameter)
        {
            if (string.IsNullOrEmpty(indexName) && isIndexParameter)
            {
                throw new Exception("TsCodeParameterDeclarationExpression indexName is null");
            }
            _isIndexParameter = isIndexParameter;
            _indexName = indexName;
            //if index type is string we create an index type reference (otherwise we will create a number)
            _indexType = isIndexTypeString ? new TsCodeTypeReference(typeof (string)) : new TsCodeTypeReference(typeof (int));
        }
        #endregion

        /// <summary>
        /// Attributes
        /// </summary>
        private TsTypeAttributes _attributes = TsTypeAttributes.None;
        public TsTypeAttributes Attributes
        {
            set { _attributes = value; }
            get { return _attributes; }
        }
        /// <summary>
        /// IsNullalbe
        /// </summary>
        public bool IsNullable { set; get; }
        /// <summary>
        /// If its an index parameter (name is not required)
        /// </summary>
        public bool _isIndexParameter { set; get; }
        public string _indexName { set; get; }
        public TsCodeTypeReference _indexType { set; get; }
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }         
        /// <summary>
        /// Types
        /// </summary>
        private readonly TsCodeTypeReferenceCollection _types = new TsCodeTypeReferenceCollection();
        public TsCodeTypeReferenceCollection Types
        {
            get { return _types; }
        }

        #region override
        /// <summary>
        /// Get Source
        /// attribute variablename : Type
        /// </summary>
        /// <returns></returns>
        internal override string GetSource(TsGeneratorOptions options, TsWriteInformation info)
        {
            //check if there are types defined
            if (!Types.Any())
            {
                throw new Exception(string.Format("No Types for Parameterdeclaration ({0}) defined", Name));
            }
            //combine types
            string typeSource = string.Join(TsDomConstants.MULTIPLETYPE_SEPERATOR, Types.Select(el => el.TsTypeName));
            //set type
            if (_isIndexParameter)
            {
                return string.Format(TsDomConstants.TS_ELEMENT_TYPE_INDEX_FORMAT, _indexName, _indexType.TsTypeName,
                    typeSource);
            }
            //if its not an index parameter
            else
            {
                string source = Name;
                //add attribute if its available
                if (Attributes != TsTypeAttributes.None)
                {
                    source = TsTypeAttributeMappings.TypeMappings[Attributes] + TsDomConstants.ATTRIBUTE_SEPEARATOR + source;
                }
                //add nullable
                if (IsNullable)
                {
                    source = string.Format(TsDomConstants.NULLABLE_FORMAT, source);
                }
                return string.Format(TsDomConstants.TS_ELEMENT_TYPE_FORMAT, source, typeSource);
            }
        }
        #endregion
    }
}
