using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TsCodeDom.Entities
{
    public abstract class TsCollectionBase<T> : IEnumerable<T>
    {
        /// <summary>
        /// List
        /// </summary>
        private List<T> List = new List<T>();


        public T this[int index]
        {
            get { return (T)List[index]; }
            set { List[index] = value; }
        }

        /// <summary>
        /// Add
        /// </summary>
        public void Add(T codeType)
        {
            List.Add(codeType);
        }
        /// <summary>
        /// Remove
        /// </summary>
        /// <param name="value"></param>
        public void Remove(T value)
        {
            List.Remove(value);
        }
        /// <summary>
        /// Contains
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Contains(T value)
        {
            return (List.Contains(value));
        }

        #region IEnumerable
        /// <summary>
        /// GetEnumerator
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            return List.GetEnumerator();
        }
        /// <summary>
        /// Get enumerator
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return List.GetEnumerator();
        }
        #endregion
    }
}
