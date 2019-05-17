using TsCodeDom.Constants;

namespace TsCodeDom.Entities
{
    public abstract class TsCodeBase
    {

        #region helper methods
        /// <summary>
        /// Add StatementBegin
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        protected string AddStatementBegin(string statement, TsGeneratorOptions options, int depth)
        {
            return statement + (options.SpaceBeforeBeginBracket ? " " : string.Empty) + TsDomConstants.STATEMENT_BRACKET_BEGIN;
        }
        /// <summary>
        /// Add Statement End
        /// </summary>
        /// <param name="statement"></param>
        /// <returns></returns>
        protected string GetStatementEnd(TsGeneratorOptions options, int depth)
        {
            return options.GetPreLineIndentString(depth) + TsDomConstants.STATEMENT_BRACKET_END;
        }
        #endregion
    }
}
