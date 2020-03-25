using ITEA_Collections.Generics.Testing;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ITEA_Collections.Generics
{
    public class IteaGenericEnumerator<T> : IEnumerator<T>
    {
        #region IEnumerator
        public List<T> Collection { get; set; }
        public IteaGenericEnumerator()
        {
            Collection = new List<T>();
        }

        private int curIndex = -1;
        public T Current { get; }
        
        object IEnumerator.Current
        {
            get { return Current; }
        }

        public void Dispose()
        {
            //Dispose(true);
            //GC.SuppressFinalize(this);
        }

        public bool MoveNext()
        {
            curIndex++;
            return (curIndex < Collection.Count && Collection[curIndex]!=null);
        }

        public void Reset()
        {
            curIndex = -1; 

        }
        #endregion
    }
}
