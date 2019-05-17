using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TsCodeDom.Constants;

namespace TsCodeDom.Entities
{
    /// <summary>
    /// CodeComment
    /// </summary>
    public class TsCodeComment : TsCodeBase
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="text"></param>
        public TsCodeComment(string text)
        {
            Text = text;
        }
        #endregion

        #region Properties and Members
        /// <summary>
        /// Text
        /// </summary>
        public string Text { get; set; }
        #endregion

        #region GetSource
        /// <summary>
        /// Get Source
        /// </summary>
        /// <param name="options"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        internal string GetSource(TsGeneratorOptions options, TsWriteInformation info)
        {
            //add begin
            var result = TsDomConstants.COMMENT_BEGIN;
            if (!string.IsNullOrEmpty(Text))
            {                
                //check if there are more lines
                var commentLines = Text.Split(new string[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);                
                //add first line
                result += commentLines.First();
                //iterate all other lines
                for (int i = 1; i < commentLines.Length; i++)
                {
                    result += Environment.NewLine + options.GetPreLineIndentString(info.Depth) + TsDomConstants.COMMENT_IN_LINE + commentLines[i];
                }
                result += TsDomConstants.COMMENT_END;
            }
            return result;
        }
        #endregion
    }
}
