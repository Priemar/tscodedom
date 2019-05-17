using TsCodeDom.Constants;

namespace TsCodeDom.Entities
{
    public class TsCodePrimitiveExpression : TsCodeExpression
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="text"></param>
        public TsCodePrimitiveExpression(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                _value = TsDomConstants.NULL_VALUE;
            }
            else
            {
                _value = string.Format(TsDomConstants.STRING_VALUE_FORMAT, value);
            }
        }
        public TsCodePrimitiveExpression(long value)
        {
            _value = value.ToString();
        }
        #endregion

        #region Properties and Members
        /// <summary>
        /// Value
        /// </summary>
        private readonly string _value = null;
        #endregion

        #region TsCodeExpression
        /// <summary>
        /// GetSource
        /// </summary>
        internal override string GetSource(TsGeneratorOptions options, TsWriteInformation info)
        {
            return _value;
        }
        #endregion
    }
}
