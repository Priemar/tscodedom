using System;
using System.Linq;
using TsCodeDom.Constants;
using TsCodeDom.Enumerations;
using TsCodeDom.Mappings;

namespace TsCodeDom.Entities
{
    /// <summary>
    /// CodeTypeReference
    /// </summary>
    public class TsCodeTypeReference : TsCodeBase
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="isNullable"></param>
        public TsCodeTypeReference(string name, TsElementTypes elementType = TsElementTypes.Class)
        {
            _name = name;
            ElementType = elementType;
        }                
        public TsCodeTypeReference(bool isNullType = false)
        {
            //use null type
            if (isNullType)
            {
                _name = TsDomConstants.NULL_VALUE;
                ElementType = TsElementTypes.Null;
            }
            //otherwise use void type
            else
            {
                _name = TsTypeMappings.TypeMappings[typeof (void).FullName];
                ElementType = TsElementTypes.Void;
            }
        }
        public TsCodeTypeReference(Type type)
        {            
            _name = type.Name;            
            //check which type
            if (type.IsEnum)
            {
                ElementType = TsElementTypes.Enumerations;                
            }
            else if (type.IsInterface)
            {
                ElementType = TsElementTypes.Interface;                
            }
            else if (type == typeof (object))
            {
                ElementType = TsElementTypes.Class;
                _name = TsTypeMappings.TypeMappings[type.FullName];
            }
            else if (TsTypeMappings.TypeMappings.ContainsKey(type.FullName))
            {
                ElementType = TsElementTypes.Primitive;
                _name = TsTypeMappings.TypeMappings[type.FullName];
            }
            else if (type.IsArray)
            {
                //set array
                IsArray = true;
                //get nested type
//                ...ElementType = TsElementTypes.Array;
#warning todo                
                throw new NotImplementedException("Array not implemented");
            }
            else
            {
                ElementType = TsElementTypes.Class;
            }
        }
        public TsCodeTypeReference(TsCodeTypeInlineMethod inlineMethodType)
        {
            ElementType = TsElementTypes.Method;
            _inlineMethodType = inlineMethodType;
        }
        public TsCodeTypeReference(TsCodeTypeInlineReference inlineTypeReference)
        {
            ElementType = TsElementTypes.Class;
            _inlineTypeReference = inlineTypeReference;
        }
        #endregion

        #region Properties and Members
        /// <summary>
        /// Inline MethodType
        /// </summary>
        private readonly TsCodeTypeInlineMethod _inlineMethodType = null;
        /// <summary>
        /// Inline type Reference
        /// </summary>
        private readonly TsCodeTypeInlineReference _inlineTypeReference = null;
        /// <summary>
        /// TypeArguemnts
        /// </summary>
        private TsCodeTypeReferenceCollection _typeArguments = new TsCodeTypeReferenceCollection();
        public TsCodeTypeReferenceCollection TypeArguments
        {
            get { return _typeArguments; }
        }
        /// <summary>
        /// Properties
        /// </summary>
        private string _name = null;
        public string Name
        {
            get
            {
                return _name;
            }
            internal set { _name = value; }
        }
        /// <summary>
        /// Type
        /// </summary>
        private Type _type = null;
        internal Type Type
        {
            get { return _type; }
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
        /// IsArray
        /// </summary>
        public bool IsArray { set; get; }
        /// <summary>
        /// TsTypename
        /// combined typename for typescript
        /// </summary>
        internal string TsTypeName
        {
            get
            {
                var typeName = Name;
                //if element type is method
                if (ElementType == TsElementTypes.Method)
                {
                    if (_inlineMethodType == null)
                    {
                        throw new Exception("CodeTypeReference ElementType = Method, InlineMethodType not set!");
                    }
                    //create method type
                    typeName = _inlineMethodType.TsTypeName;
                    //TODO Implement array gen.
                }
                //if we have a inline type reference, we will use this
                else if (ElementType == TsElementTypes.Class && _inlineTypeReference!=null)
                {
                    typeName = _inlineTypeReference.TsTypeName;
                }

                //if not var, let, void
                if (ElementType != TsElementTypes.Var && ElementType != TsElementTypes.Let && ElementType != TsElementTypes.Void)
                {
                    //if there are typearguments add them to type
                    if (TypeArguments.Any())
                    {
                        var typeArgs = string.Join(TsDomConstants.LIST_ELEMENT_SEPERATOR,
                            TypeArguments.Select(el => el.TsTypeName));
                        //create generic typename
                        typeName = string.Format(TsDomConstants.TS_GENERIC_TYPE_FORMAT, typeName, typeArgs);
                    }
                    //special name for array
                    if (IsArray)
                    {
                        return string.Format(TsDomConstants.TS_ARRAY_FORMAT, typeName);
                    }
                }   
                //return element type
                return typeName;
            }
        }
        #endregion
    }
}
