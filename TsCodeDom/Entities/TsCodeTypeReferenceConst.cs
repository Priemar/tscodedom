using TsCodeDom.Constants;
using TsCodeDom.Enumerations;

namespace TsCodeDom.Entities
{
    public class TsCodeTypeReferenceConst : TsCodeTypeReference
    {
        public TsCodeTypeReferenceConst()
        {
            ElementType = TsElementTypes.Const;
            Name = TsDomConstants.TYPE_CONST;
        }
    }
}
