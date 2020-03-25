using System;
using System.Collections;
using System.Collections.Generic;

namespace ITEA_Collections.Generics
{
    public class IteaGenericCollection<T> : IEnumerable<T>, IBaseGenericCollectionUsing<T>
    {
        public List<T> collection { get; set; }

        public IteaGenericCollection()
        {
            collection = new List<T>();
        }


        #region IBaseGenericCollectionUsing
        public void Add(T ts)
        {
            collection.Add(ts);
        }

        public void AddMany(T[] ts)
        {
            collection.AddRange(ts);

        }

        public void Clear()
        {
            collection.Clear();
        }

        public T[] GetAll()
        {
            return collection.GetRange(0, collection.Count).ToArray();
        }

        public T GetByID(int index)
        {
            try
            {
                return collection[index];
            }
            catch (Exception except)
            {
                Console.WriteLine(except.GetType().Name + except.Message);
                Console.WriteLine($"there is no element with index: {index}", ConsoleColor.Red);
                // return null;
                return collection[0];
            }
        }

        public void RemoveByID(int index)
        {
            try
            {
                collection.RemoveAt(index);
                //collection[index] = null;
                Console.WriteLine($"The element index {index} is removed.");
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine($"There is no element with index: {index}", ConsoleColor.Red);
            }
        }

        public void ShowAll()
        {
            foreach (T a in collection)
                Console.WriteLine($"{a}", ConsoleColor.DarkBlue);
        }
        #endregion

        #region IEnumerable
        public IEnumerator<T> GetEnumerator()
        {
            return collection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (var item in collection)
                yield return item;
        }
        #endregion
    }
}

