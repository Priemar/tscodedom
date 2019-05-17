using System;
using System.Collections.Generic;
using System.Linq;

namespace TsCodeDom.Mappings
{
    /// <summary>
    /// TypeMappings
    /// </summary>
    internal static class TsTypeMappings
    {
        internal static readonly Dictionary<string, string> TypeMappings = new Dictionary<string, string>()
        {
            {typeof(int).FullName, "number"},
            {typeof(uint).FullName, "number"},
            {typeof(long).FullName, "number"},
            {typeof(ulong).FullName, "number"},
            {typeof(short).FullName, "number"},
            {typeof(ushort).FullName, "number"},
            {typeof(float).FullName, "number"},
            {typeof(double).FullName, "number"},
            {typeof(decimal).FullName, "number"},
            {typeof(byte).FullName, "number"},
            {typeof(sbyte).FullName, "number"},
            {typeof(string).FullName, "string"},
            {typeof(char).FullName, "string"},
            {typeof(Guid).FullName, "string"},
            {typeof(bool).FullName, "boolean"},
            {typeof(void).FullName, "void"},
            {typeof(object).FullName, "any"},
            {typeof(DateTime).FullName, "Date"},
            {typeof(DateTimeOffset).FullName, "Date"}            
            //TimeSpan is not so supported in Javascript
        };
        /// <summary>
        /// ArrayTypenames
        /// </summary>
        internal static readonly System.Collections.Generic.HashSet<string> ArrayTypeNames = new System.Collections.Generic.HashSet<string>(
            new string[] {
                typeof(IEnumerable<>).FullName,
                typeof(IList<>).FullName,
                typeof(ICollection<>).FullName,
                typeof(IQueryable<>).FullName,
                typeof(IReadOnlyList<>).FullName,
                typeof(List<>).FullName,
                typeof(System.Collections.ObjectModel.Collection<>).FullName,
                typeof(IReadOnlyCollection<>).FullName
           }
       );
    }
}
