using System;
using System.Linq;
using TsCodeDom.Constants;

namespace TsCodeDom.Entities
{
    public class TsCodeConditionStatement : TsCodeStatement
    {
        public TsCodeExpression Condition { set; get; }
        /// <summary>
        /// False Statements
        /// </summary>
        private TsCodeStatementCollection _falseStatements = new TsCodeStatementCollection();
        public TsCodeStatementCollection FalseStatements
        {
            get { return _falseStatements; }
            set { _falseStatements = value; }
        }
        /// <summary>
        /// True Statements
        /// </summary>
        private TsCodeStatementCollection _trueStatements = new TsCodeStatementCollection();
        public TsCodeStatementCollection TrueStatements
        {
            get { return _trueStatements; }
            set { _trueStatements = value; }
        }

        #region Override
        /// <summary>
        /// Write Source
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="options"></param>
        /// <param name="info"></param>
        internal override void WriteSource(System.IO.StreamWriter writer, TsGeneratorOptions options, TsWriteInformation info)
        {
            //sec check
            if (Condition == null)
            {
                throw new ArgumentNullException("Condition in TsConditionStatement not set");
            }
            //get condition source
            string conditionSource = Condition.GetSource(options, info);
            //create statement
            string statementSource = options.GetPreLineIndentString(info.Depth) + string.Format(TsDomConstants.TS_IF_STATEMENT_FORMAT, conditionSource);
            //add start bracket for true condition
            statementSource += TsDomConstants.STATEMENT_BRACKET_BEGIN;
            //write
            writer.WriteLine(statementSource);
            //if there are true statements (add them)
            if (TrueStatements.Any())
            {
                //write statements
                TrueStatements.ToList().ForEach(el=>el.WriteSource(writer, options, info.Clone(info.Depth+1)));
            }

            //write end bracket
            writer.WriteLine(GetStatementEnd(options, info.Depth));
        }
        #endregion
    }
}
