using TsCodeDom.Constants;

namespace TsCodeDom.Entities
{
    public class TsCodeVariableReferenceExpression : TsCodeExpression
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        public TsCodeVariableReferenceExpression(string name) : this(name, null)
        {            
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="objectKey"></param>
        public TsCodeVariableReferenceExpression(string name, TsCodeExpression objectKey)
        {
            Name = name;
            ObjectKey = objectKey;
        }
        #endregion
        /// <summary>
        /// Name
        /// </summary>
        public string Name { set; get; }
        public TsCodeExpression ObjectKey { set; get; }

        #region Override
        /// <summary>
        /// GetSource
        /// </summary>
        /// <param name="options"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        internal override string GetSource(TsGeneratorOptions options, TsWriteInformation info)
        {
            var source = Name;
            //if we need to get the object key
            if (ObjectKey!=null)
            {
                source = string.Format(TsDomConstants.TS_OBJECT_KEY_FORMAT, Name, ObjectKey.GetSource(options, info));
            }
            return source;
        }
        #endregion
    }
}
