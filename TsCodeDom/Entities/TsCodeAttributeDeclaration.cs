using TsCodeDom.Constants;

namespace TsCodeDom.Entities
{
    public class TsCodeAttributeDeclaration
    {
        public string Name { get; set; }

        /// <summary>
        /// Return source
        /// </summary>
        /// <returns></returns>
        public string GetSource()
        {
            //prepare (actually we are not supporting decorator parameters, thats why we are unsing string.Empty here)            
            return string.Format(TsDomConstants.TS_DECORATOR_FORMAT, Name, string.Empty);
        }
    }
}
