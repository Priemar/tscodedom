using TsCodeDom.Constants;

namespace TsCodeDom.Entities
{
    public class TsCodeAssignStatement : TsCodeStatement
    {
        public TsCodeExpression Left { get; set; }
        public TsCodeExpression Right { get; set; }

        #region override
        /// <summary>
        /// write source
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="options"></param>
        /// <param name="info"></param>
        internal override void WriteSource(System.IO.StreamWriter writer, TsGeneratorOptions options, TsWriteInformation info)
        {
            var source = options.GetPreLineIndentString(info.Depth);
            source += string.Format(TsDomConstants.ASSIGN_FORMAT, Left.GetSource(options, info), Right.GetSource(options, info));
            source += TsDomConstants.EXPRESSION_END;
            //write
            writer.WriteLine(source);
        }
        #endregion
    }
}
