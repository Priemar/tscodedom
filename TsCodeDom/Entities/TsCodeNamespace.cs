using System.Linq;
using TsCodeDom.Constants;

namespace TsCodeDom.Entities
{
    public class TsCodeNamespace : TsCodeWriteBase
    {
        #region Properties and Members
        /// <summary>
        /// Imports
        /// </summary>
        private TsCodeNamespaceImportCollection _imports = new TsCodeNamespaceImportCollection();
        public TsCodeNamespaceImportCollection Imports
        {
            get { return _imports; }
        }
        /// <summary>
        /// Comments
        /// </summary>
        private readonly TsCodeCommentStatementCollection _comments = new TsCodeCommentStatementCollection();
        public TsCodeCommentStatementCollection Comments
        {
            get { return _comments; }
        }
        /// <summary>
        /// Types
        /// </summary>
        private TsCodeTypeDeclarationCollection _types = new TsCodeTypeDeclarationCollection();
        public TsCodeTypeDeclarationCollection Types
        {
            get { return _types; }
        }
        /// <summary>
        /// Name
        /// </summary>
        public string Name { set; get; }
        #endregion

        #region Helper Properties
        /// <summary>
        /// HasName
        /// </summary>
        private bool HasName
        {
            get { return !string.IsNullOrEmpty(Name); }
        }
        #endregion

        #region TsCodeBase
        /// <summary>
        /// Write Source
        /// </summary>
        /// <param name="writer"></param>
        internal override void WriteSource(System.IO.StreamWriter writer, TsGeneratorOptions options, TsWriteInformation info)
        {          
            //if there are comments write them
            if (Comments.Any())
            {
                Comments.ToList().ForEach(el=>el.WriteSource(writer, options, info));
            }
            //if there is a name
            if (HasName)
            {
                //add statement begin
                var namespaceSource = AddStatementBegin(string.Format(TsDomConstants.TS_NAMESPACE_FORMAT, Name), options,info.Depth);
                //write statement line
                writer.WriteLine(namespaceSource);
                //add imports
                Imports.ToList().ForEach(el => el.WriteSource(writer, options, info.Clone(info.Depth+1)));
                //write content
                WriteContent(writer, options, info.Clone(info.Depth+1));
                //write close statement
                writer.WriteLine(GetStatementEnd(options, info.Depth));
            }
            //if there is no name just add content
            else
            {
                //add imports
                Imports.ToList().ForEach(el => el.WriteSource(writer, options, info));
                WriteContent(writer, options, info.Clone());
            }
        }
        #endregion

        #region Helper Methods
        /// <summary>
        /// Write Content
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="options"></param>
        /// <param name="depth"></param>
        private void WriteContent(System.IO.StreamWriter writer, TsGeneratorOptions options, TsWriteInformation info)
        {
            //add types source
            foreach (var item in Types)
            {
                //write type source
                item.WriteSource(writer, options, info);
            }            
        }
        #endregion
    }
}
