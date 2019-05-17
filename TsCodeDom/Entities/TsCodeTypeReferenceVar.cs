using TsCodeDom.Constants;
using TsCodeDom.Enumerations;

namespace TsCodeDom.Entities
{
    public class TsCodeTypeReferenceVar : TsCodeTypeReference
    {
        public TsCodeTypeReferenceVar()
        {
            ElementType = TsElementTypes.Var;
            Name = TsDomConstants.TYPE_VAR;
        }
    }
}
