using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TsCodeDom.Entities
{
    public class TsCodeMemberComment : TsCodeTypeMember
    {
        public TsCodeMemberComment(TsCodeComment comment)
        {
            Comment = comment;
        }
        #region Properties and Members
        /// <summary>
        /// Comment
        /// </summary>
        public TsCodeComment Comment { set; get; }
        #endregion

        #region override
        /// <summary>
        /// Write Source
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="options"></param>
        /// <param name="info"></param>
        internal override void WriteSource(System.IO.StreamWriter writer, TsGeneratorOptions options, TsWriteInformation info)
        {
            if (Comment == null)
            {
                throw new ArgumentNullException("Comment in TsCodeMemberComment is null");
            }
            //write comment
            var source = options.GetPreLineIndentString(info.Depth);
            source += Comment.GetSource(options, info);
            writer.WriteLine(source);
        }
        #endregion
    }
}
