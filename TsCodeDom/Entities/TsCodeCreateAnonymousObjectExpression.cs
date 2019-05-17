using System;
using System.Linq;
using TsCodeDom.Constants;
using TsCodeDom.Enumerations;

namespace TsCodeDom.Entities
{
    /// <summary>
    /// Create Anonymous Object
    /// </summary>
    public class TsCodeCreateAnonymousObjectExpression : TsCodeExpression
    {
        /// <summary>
        /// Properties
        /// </summary>
        private readonly TsCodeTypeMemberCollection _properties = new TsCodeTypeMemberCollection();
        public TsCodeTypeMemberCollection Properties
        {            
            get { return _properties; }
        }
        /// <summary>
        /// Defines if the membername is defined as string ("<membername") or without string comment chars
        /// </summary>
        public bool MemberNameAsString { set; get; }

        #region override
        /// <summary>
        /// GetSource
        /// </summary>
        /// <returns></returns>
        internal override string GetSource(TsGeneratorOptions options, TsWriteInformation info)
        {
            var source = TsDomConstants.STATEMENT_BRACKET_BEGIN;
            //if there are properties
            if (Properties.Any())
            {
                var newInfo = info.Clone(info.Depth + 1);
                newInfo.ForType = TsElementTypes.InlineObject;
                newInfo.MemberNameAsString = MemberNameAsString;
                //add new Line
                source += Environment.NewLine;
                Properties.ToList().ForEach(el => source += el.GetStringFromWriteSource(options, newInfo));
                source += options.GetPreLineIndentString(info.Depth);
            }
            source+=TsDomConstants.STATEMENT_BRACKET_END;
            //return
            return source;
        }
        #endregion
    }
}
