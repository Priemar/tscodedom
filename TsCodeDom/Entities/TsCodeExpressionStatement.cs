using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TsCodeDom.Constants;

namespace TsCodeDom.Entities
{
    public class TsCodeExpressionStatement : TsCodeStatement
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="expression"></param>
        public TsCodeExpressionStatement(TsCodeExpression expression)
        {
            Expression = expression;
        }
        #endregion

        #region properties and members
        /// <summary>
        /// Expressoin
        /// </summary>
        public TsCodeExpression Expression { set; get; }
        #endregion

        #region override
        /// <summary>
        /// WriteSource
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="options"></param>
        /// <param name="info"></param>
        internal override void WriteSource(System.IO.StreamWriter writer, TsGeneratorOptions options, TsWriteInformation info)
        {
            //sec check
            if (Expression == null)
            {
                throw new Exception(string.Format("TsCodeExpressionStatement: Expression is null"));
            }
            var source = options.GetPreLineIndentString(info.Depth);           
            source += Expression.GetSource(options, info) + TsDomConstants.EXPRESSION_END;
            //write
            writer.WriteLine(source);
        }
        #endregion
    }
}
