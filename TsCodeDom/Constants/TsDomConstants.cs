namespace TsCodeDom.Constants
{
    internal static class TsDomConstants
    {
        /// <summary>
        /// Statement Begin / End,  Expression End
        /// </summary>
        internal const string STATEMENT_BRACKET_BEGIN = "{";
        internal const string STATEMENT_BRACKET_END = "}";
        internal const string EXPRESSION_END = ";";
        internal const string LIST_ELEMENT_SEPERATOR = ",";
        internal const string PARAMETER_SEPERATOR = ", ";
        internal const string MULTIPLETYPE_SEPERATOR = " | ";
        internal const string START_SIGN = "*";
        internal const string ATTRIBUTE_SEPEARATOR = " ";
        private const string ASSIGN_SIGN = "=";
        private const string DOT_SEPERATOR = ".";

        /// <summary>
        /// Comment
        /// </summary>
        internal const string COMMENT_BEGIN = "/** ";
        internal const string COMMENT_IN_LINE = "  * ";
        internal const string COMMENT_END = " */";
        /// <summary>
        /// This value
        /// </summary>
        internal const string THIS_VALUE = "this";
        /// <summary>
        /// Super value
        /// </summary>
        internal const string SUPER_VALUE = "super";
        /// <summary>
        /// Null value
        /// </summary>
        internal const string NULL_VALUE = "null";
        /// <summary>
        /// VarType
        /// </summary>
        internal const string TYPE_LET = "let";
        internal const string TYPE_VAR = "var";
        internal const string TYPE_CONST = "const";
        /// <summary>
        /// Getter Format
        /// 0: propertyname
        /// 1: type
        /// </summary>
        internal const string GETTER_FORMAT = "get {0}(): {1}";
        /// <summary>
        /// Setter Format
        /// 0: propertyname
        /// 1: value parameter name
        /// 2: value parameter type
        /// </summary>
        internal const string SETTER_FORMAT = "set {0}({1}: {2})";
        /// <summary>
        /// Method Return format
        /// 0: expression
        /// </summary>
        internal const string TS_METHOD_RETURN_FORMAT = "return {0}";
        /// <summary>
        /// Generic Type format
        /// 0: Typename
        /// 1: Generic Arguments
        /// </summary>
        internal const string TS_GENERIC_TYPE_FORMAT = "{0}<{1}>";
        /// <summary>
        /// Curly inline Brackets
        /// 0: code
        /// </summary>
        internal const string CURLY_INLINE_BRACKETS_FORMAT = "{{{0}}}"; 
        /// <summary>
        /// String Value Format
        /// </summary>
        internal const string STRING_VALUE_FORMAT = "'{0}'";
        /// <summary>
        /// Assign
        /// 0: left
        /// 1: right
        /// </summary>
        internal const string ASSIGN_FORMAT = "{0} " + ASSIGN_SIGN + " {1}";
        /// <summary>
        /// Import type as format
        /// 0: import type name
        /// 1: alias name
        /// </summary>
        internal const string IMPORT_TYPE_AS_FORMAT = "{0} as {1}";
        /// <summary>
        /// FieldReferenceFormat
        /// 0: TargetObject
        /// 1: Sub (Element / Method)
        /// </summary>
        internal const string ELEMENT_SUB_REFERENCE_FORMAT = "{0}" + DOT_SEPERATOR + "{1}"; 
        /// <summary>
        /// Import format
        /// 0: import types, comma seperated
        /// 1: relative path
        /// 
        /// escape curly brackets = 2xcurly bracket
        /// </summary>
        internal const string TS_IMPORT_FORMAT = "import {0} from '{1}'";
        /// <summary>
        /// Export statment format
        /// 0: types
        /// 1: path
        /// </summary>
        internal const string TS_EXPORT_STATEMENT_FORMAT = "export {0} from '{1}'";
        /// <summary>
        /// Array
        /// 0: type
        /// </summary>
        internal const string TS_ARRAY_FORMAT = "{0}[]";
        /// <summary>
        /// BaseTypeSeperator
        /// </summary>
        internal const string TS_BASETYPE_SEPERATOR = ",";
        /// <summary>
        /// NullableFormat
        /// </summary>
        internal const string NULLABLE_FORMAT = "{0}?";
        /// <summary>
        /// BaseType Format
        /// 0: default type
        /// 1: basetype list
        /// </summary>
        internal const string TS_BASETYPE_EXTENDS_FORMAT = "{0} extends {1}";
        /// <summary>
        /// BaseType Format
        /// 0: default type
        /// 1: basetype list
        /// </summary>
        internal const string TS_BASETYPE_IMPLEMENTS_FORMAT = "{0} implements {1}";        
        /// <summary>
        /// Namespace
        /// 0: Namespace Name
        /// </summary>
        internal const string TS_NAMESPACE_FORMAT = "namespace {0}";
        /// <summary>
        /// Constructor constant
        /// </summary>
        internal const string TS_CONSTRUCTOR_NAME = "constructor";
        /// <summary>
        /// Attribute Combine format
        /// 0: typeattribute text
        /// 1: statement
        /// </summary>
        internal const string TS_ATTRIBUTE_COMBINE_FORMAT = "{0} {1}";
        /// <summary>
        /// Type Class Format
        /// 0: name
        /// </summary>
        internal const string TS_TYPE_CLASS_FORMAT = "class {0}";
        internal const string TS_TYPE_INTERFACE_FORMAT = "interface {0}";
        internal const string TS_TYPE_ENUM_FORMAT = "enum {0}";
        internal const string TS_TYPE_CONST_FORMAT = "const {0}";
        /// <summary>
        /// Export Format
        /// 0: type
        /// </summary>
        internal const string TS_EXPORT_FORMAT = "export {0}";
        /// <summary>
        /// abstract keyword
        /// </summary>
        internal const string TS_ABSTRACT_FORMAT = "abstract {0}";
        /// <summary>
        /// Class Field format
        /// 0: Name
        /// 1: Type
        /// </summary>
        internal const string TS_CLASS_FIELD_FORMAT = "{0}: {1}";
        /// <summary>
        /// Code MemberField Format
        /// 0: Name
        /// 1: Types
        /// </summary>
        internal const string TS_ELEMENT_TYPE_FORMAT = "{0}: {1}";
        /// <summary>
        /// Element index type format
        /// 0: index name
        /// 1: index type (can only be string or number)
        /// 2: value type
        /// </summary>
        internal const string TS_ELEMENT_TYPE_INDEX_FORMAT = "[ {0}: {1} ]: {2}";
        /// <summary>
        /// Constant SetValue Statement
        /// 0: ConstantName
        /// 1: Value
        /// </summary>
        internal const string TS_CONSTANT_SETVALUE_FORMAT = "{0}: {1}";
        /// <summary>
        /// InlineObject
        /// 0: variablename
        /// 1: Value
        /// </summary>
        internal const string TS_INLINEOBJECT_SETVALUE_FORMAT = "{0}: {1}";
        /// <summary>
        /// Decorator format
        /// 0: Decorator name
        /// 1: Attributes
        /// </summary>
        internal const string TS_DECORATOR_FORMAT = "@{0}({1})";
        /// <summary>
        /// MemberMethod Format
        /// 0: Member Method Type + Name
        /// 1: Parameters
        /// </summary>
        internal const string TS_MEMBERMETHOD_FORMAT = "{0}({1})";
        /// <summary>
        /// IfStatement Format
        /// 0: Condition
        /// </summary>
        internal const string TS_IF_STATEMENT_FORMAT = "if ({0})";
        /// <summary>
        /// Parameter Type Method:  if Method used as parameter
        /// 0: parameters
        /// 1: return type
        /// </summary>
        internal const string TS_PARAMETER_TYPE_METHOD_FORMAT = "({0}) => {1}";
        /// <summary>
        /// Inline class parameter (we need 2 curly braces to escape)
        /// 0: parameters
        /// </summary>
        internal const string TS_PARAMETER_TYPE_INLINE_CLASS_FORMAT = "{{ {0} }}";
        /// <summary>
        /// Binary Operator Expression Format
        /// 0: Left
        /// 1: Operator
        /// 2: Right
        /// </summary>
        internal const string TS_BINARYOPERATOR_EXPRESSION_FORMAT = "({0} {1} {2})";

        /// <summary>
        /// Object Key Format
        /// 0: object
        /// 1: key
        /// </summary>
        internal const string TS_OBJECT_KEY_FORMAT = "{0}[{1}]";
    }
}
