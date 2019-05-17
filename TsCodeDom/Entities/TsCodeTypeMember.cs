using System.CodeDom;
using System.IO;
using System.Linq;
using TsCodeDom.Enumerations;

namespace TsCodeDom.Entities
{
    public abstract class TsCodeTypeMember : TsCodeWriteBase
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Comments
        /// </summary>
        private readonly TsCodeCommentStatementCollection _comments = new TsCodeCommentStatementCollection();
        public TsCodeCommentStatementCollection Comments
        {
            get { return _comments; }
        }
        /// <summary>
        /// Decorators
        /// </summary>
        private TsCodeAttributeDeclarationTsCollection _decorators = new TsCodeAttributeDeclarationTsCollection();
        public TsCodeAttributeDeclarationTsCollection Decorators
        {
            get { return _decorators; }
        }
        /// <summary>
        /// Attributes
        /// </summary>
        private TsMemberAttributes _attributes = TsMemberAttributes.None;
        public TsMemberAttributes Attributes
        {
            get { return _attributes; }
            set { _attributes = value; }
        }

        #region override
        /// <summary>
        /// WriteSource
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="options"></param>
        /// <param name="info"></param>
        internal override void WriteSource(StreamWriter writer, TsGeneratorOptions options, TsWriteInformation info)
        {
            //if add blank lines option is set, add a empty line
            if (options.BlankLinesBetweenMembers)
            {
                writer.WriteLine();
            }
#warning write custom attriubtes here not in inherited classes
            //write comments if exists
            if (Comments.Any())
            {
                Comments.ToList().ForEach(el=>el.WriteSource(writer, options, info));
            }
        }

        #endregion
    }
}
