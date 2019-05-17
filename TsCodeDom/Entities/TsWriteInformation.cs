using System;
using TsCodeDom.Enumerations;

namespace TsCodeDom.Entities
{
    internal class TsWriteInformation
    {
        internal TsWriteInformation (int depth)
        {
            if (depth < 0)
            {
                throw new Exception("Depth < 0");
            }
            _depth = depth;
        }
    
        /// <summary>
        /// Properties
        /// </summary>
        private readonly int _depth = 0;
        public int Depth { get { return _depth; } }     
        public TsElementTypes ForType { set; get; }
        public bool MemberNameAsString { set; get; }

        /// <summary>
        /// Clone
        /// </summary>
        /// <returns></returns>
        public TsWriteInformation Clone()
        {
            return Clone(Depth);
        }
        public TsWriteInformation Clone(int depth)
        {
            return new TsWriteInformation(depth)
            {
                ForType = ForType
            };
        }
    }
}
