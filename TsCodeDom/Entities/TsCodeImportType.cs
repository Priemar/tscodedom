using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TsCodeDom.Constants;

namespace TsCodeDom.Entities
{
    /// <summary>
    /// TsCodeImport Type
    /// </summary>
    public class TsCodeImportType
    {
        #region properties and members
        /// <summary>
        /// Name
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// Alias
        /// </summary>
        public string Alias { set; get; }
        #endregion

        #region helper
        /// <summary>
        /// Is Import all
        /// </summary>
        public bool IsImportAll
        {
            get
            {
                return string.IsNullOrEmpty(Name) || Name == TsDomConstants.START_SIGN;
            }
        }
        #endregion

        #region TsCodeWriteBase
        /// <summary>
        /// Write Source
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="options"></param>
        /// <param name="info"></param>
        internal string GetSource()
        {
            //use start sign ('all') to if name is not set
            var name = string.IsNullOrEmpty(Name) ? TsDomConstants.START_SIGN : Name;
            // if there isn't an alias defined, use the name as it is, otherwise format the name and alias
            return string.IsNullOrEmpty(Alias) ? name : string.Format(TsDomConstants.IMPORT_TYPE_AS_FORMAT, name, Alias);
        }
        #endregion
    }
}
