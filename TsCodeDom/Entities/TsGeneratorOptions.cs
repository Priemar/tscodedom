using System;
using System.Text;

namespace TsCodeDom.Entities
{
    public class TsGeneratorOptions
    {
        #region Properties and Members
        /// <summary>
        /// Indent String
        /// </summary>
        private string _indentString = "    ";
        public string IndentString
        {
            set { _indentString = value; }
            get { return _indentString; }
        }

        /// <summary>
        /// Blank lines except for member fields
        /// </summary>
        private bool _blankLinesBetweenMembers = true;
        public bool BlankLinesBetweenMembers
        {
            set { _blankLinesBetweenMembers = value; }
            get { return _blankLinesBetweenMembers; }
        }

        /// <summary>
        /// Space before begin bracket
        /// </summary>
        private bool _spaceBeforeBeginBracket = true;
        public bool SpaceBeforeBeginBracket
        {
            set { _spaceBeforeBeginBracket = value; }
            get { return _spaceBeforeBeginBracket; }
        }
        #endregion

        #region helper
        /// <summary>
        /// GetPre Line Indent String
        /// </summary>
        /// <param name="depth"></param>
        /// <returns></returns>
        internal string GetPreLineIndentString(int depth)
        {
            if (depth < 0)
            {
                throw new Exception("Line depth is smaller than 0");
            }
            if (IndentString == null)
            {
                return string.Empty;
            }
            StringBuilder sb = new StringBuilder();
            //Iterate depth
            for (int i = 0; i < depth; i++)
            {
                sb.Append(IndentString);
            }
            return sb.ToString();
        }
        /// <summary>
        /// Clone
        /// </summary>
        /// <returns></returns>
        internal TsGeneratorOptions Clone(string indentString, bool blankLinesBetweenMembers)
        {
            return new TsGeneratorOptions()
            {
                IndentString = indentString,
                BlankLinesBetweenMembers = blankLinesBetweenMembers
            };
        }
        #endregion
    }
}
