using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TsCodeDom.Constants;
using TsCodeDom.Enumerations;
using TsCodeDom.Mappings;

namespace TsCodeDom.Entities
{
    public class TsCodeMemberProperty : TsCodeTypeMember
    {

        #region constructor
        /// <summary>
        /// Name
        /// </summary>
        /// <param name="name"></param>
        public TsCodeMemberProperty(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("TsCodeMemberProperty");
            }
            Name = name;
        }
        #endregion

        #region Properties and Members
        /// <summary>
        /// Name
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// SetParameter Name
        /// </summary>
        public string SetParameterName { set; get; }
        /// <summary>
        /// HasSet
        /// </summary>
        public bool HasGet { get; set; }
        /// <summary>
        /// HasGet
        /// </summary>
        public bool HasSet { get; set; }
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
        /// <summary>
        /// SetStatements
        /// </summary>
        private readonly TsCodeStatementCollection _setStatements = new TsCodeStatementCollection();
        public TsCodeStatementCollection SetStatements
        {
            get { return _setStatements; }
        }
        /// <summary>
        /// GetStatements
        /// </summary>
        private readonly TsCodeStatementCollection _getStatements = new TsCodeStatementCollection();
        public TsCodeStatementCollection GetStatements
        {
            get { return _getStatements; }
        }
        #endregion

        #region TsWriteCodeBase
        /// <summary>
        /// Write Source
        /// </summary>
        /// <param name="writer"></param>
        internal override void WriteSource(System.IO.StreamWriter writer, TsGeneratorOptions options, TsWriteInformation info)
        {
            //write base stuff
            base.WriteSource(writer, options.Clone(options.IndentString, false), info);   
            //sec check
            if (info.ForType != TsElementTypes.Class)
            {
                throw new Exception("TsCodeMemberProperty can only be defined for class");
            }
            if (!Types.Any())
            {
                throw new Exception("TsCodeMemberProperty type not defined");
            }
            //prepare source
            var source = options.GetPreLineIndentString(info.Depth);
            //add attributres
            if (Attributes != TsMemberAttributes.None)
            {
                source += TsMemberAttributeMappings.TypeMappings[Attributes] + TsDomConstants.ATTRIBUTE_SEPEARATOR;
            }
            //if there is no setter and getter this is a bit useless
            if (!HasSet && !HasGet)
            {
                throw new Exception("TsCodeMemberProperty there is no setter and getter for MemberProperty"+ Name);
            }
            //if we have a getter write getter
            if (HasGet)
            {
                var getSource = source + string.Format(TsDomConstants.GETTER_FORMAT, Name, GetTypeSource()) + TsDomConstants.STATEMENT_BRACKET_BEGIN;
                //write begin line
                writer.WriteLine(getSource);
                //write statemetns
                GetStatements.ToList().ForEach(el => el.WriteSource(writer, options, info.Clone(info.Depth + 1)));
                //write end source
                writer.WriteLine(GetStatementEnd(options, info.Depth));
            }
            //as setter
            if (HasSet)
            {
                var setSource = source + string.Format(TsDomConstants.SETTER_FORMAT, Name, SetParameterName, GetTypeSource())+TsDomConstants.STATEMENT_BRACKET_BEGIN;
                //write begin line
                writer.WriteLine(setSource);
                //write statemetns
                SetStatements.ToList().ForEach(el => el.WriteSource(writer, options, info.Clone(info.Depth + 1)));
                //write end source
                writer.WriteLine(GetStatementEnd(options, info.Depth));
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
