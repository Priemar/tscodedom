using System;
using System.Collections.Generic;
using TsCodeDom.Constants;
using System.Linq;

namespace TsCodeDom.Entities
{
    public class TsCodeNamespaceImport : TsCodeWriteBase
    {
        /// <summary>
        /// Path
        /// </summary>
        public string Path { set; get; }
        /// <summary>
        /// Import Types
        /// </summary>
        private List<TsCodeImportType> _importTypes = new List<TsCodeImportType>();
        public List<TsCodeImportType> ImportTypes { get { return _importTypes; } }
        /// <summary>
        /// Defines if its an export or import
        /// </summary>
        public bool IsExport { set; get; }
        /// <summary>
        /// Export name
        /// </summary>
        public string ExportName { set; get; }

        #region private Methods
        /// <summary>
        /// IsValid
        /// </summary>
        /// <returns></returns>
        private bool IsValid(out string detail)
        {
            detail = null;
            return true;
        }
        #endregion

        #region TsCodeWriteBase
        /// <summary>
        /// Write Source
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="options"></param>
        /// <param name="info"></param>
        internal override void WriteSource(System.IO.StreamWriter writer, TsGeneratorOptions options, TsWriteInformation info)
        {
            //sec check
            string detailError = null;
            if (!IsValid(out detailError))
            {
                throw new Exception(detailError);
            }
            //get type string
            string typeString;
            //get typestring (if there isnt a type, use all selector)
            if (ImportTypes.Count <= 0)
            {
                typeString = TsDomConstants.START_SIGN;
            }
            else
            {
                var importTypeSourceList = ImportTypes.OrderBy(el => el.Name).Select(el => el.GetSource());
                var importTypeIsNotSpecific = ImportTypes.Count == 1 && ImportTypes[0].IsImportAll;
                // if the import type is import all and its only one type, we dont need to wrap it in brackets
                typeString = importTypeIsNotSpecific
                    ? ImportTypes[0].GetSource()
                    : string.Format(TsDomConstants.CURLY_INLINE_BRACKETS_FORMAT, string.Join(TsDomConstants.PARAMETER_SEPERATOR, importTypeSourceList));
            }

            string source = null;
            // export
            if (IsExport)
            {
                source = string.Format(TsDomConstants.TS_EXPORT_STATEMENT_FORMAT, typeString, Path);
            }
            // import
            else
            {
                source = string.Format(TsDomConstants.TS_IMPORT_FORMAT, typeString, Path);
            }
            //add end expression
            source += TsDomConstants.EXPRESSION_END;
            //write import
            writer.WriteLine(options.GetPreLineIndentString(info.Depth) + source);
        }
        #endregion
    }
}
