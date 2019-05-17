using System;
using TsCodeDom.Constants;

namespace TsCodeDom.Entities
{
    public class TsCodeMethodReturnStatement : TsCodeStatement
    {
        public TsCodeExpression Expression { set; get; }

        #region override
        /// <summary>
        /// WriteSource
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="options"></param>
        /// <param name="info"></param>
        internal override void WriteSource(System.IO.StreamWriter writer, TsGeneratorOptions options, TsWriteInformation info)
        {
            if (Expression == null)
            {
                throw new ArgumentNullException("Expression in TsCodeMethodReturnStatement is null");
            }
            var source = options.GetPreLineIndentString(info.Depth) + string.Format(TsDomConstants.TS_METHOD_RETURN_FORMAT, Expression.GetSource(options, info)) + TsDomConstants.EXPRESSION_END;
            //write source
            writer.WriteLine(source);
        }
        #endregion
    }
}
