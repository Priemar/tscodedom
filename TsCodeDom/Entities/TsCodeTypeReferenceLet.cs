using TsCodeDom.Constants;
using TsCodeDom.Enumerations;

namespace TsCodeDom.Entities
{
    public class TsCodeTypeReferenceLet : TsCodeTypeReference
    {
        public TsCodeTypeReferenceLet()
        {
            ElementType = TsElementTypes.Let;
            Name = TsDomConstants.TYPE_LET;
        }
    }
}
