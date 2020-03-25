using System;
using System.Collections.Generic;

using ITEA_Collections.Common;

namespace ITEA_Collections.Usings
{
    internal class GenericListUsing : IBaseCollectionUsing
    {
        public List<string> List { get; set; }

        public GenericListUsing()
        {
            List = new List<string>();
        }

        public void Add(object ts)
        {
            List.Add(ts.ToString());
        }

        public void AddMany(object[] ts)
        {
            if (ts is null)
                Console.WriteLine($"Your array is null!", ConsoleColor.Red);
            else
            {
                foreach (object item in ts)
                    List.Add(ts.ToString());
            }
        }

        public void Clear()
        {
            List.Clear();
        }

        public object[] GetAll()
        {
            return List.GetRange(0, List.Count).ToArray();
        }

        public object GetByID(int index)
        {
            try
            {
                return List[index];
            }
            catch (Exception except)
            {
                Console.WriteLine(except.GetType().Name + except.Message);
                Console.WriteLine($"there is no element with index: {index}", ConsoleColor.Red);
                return null;
            }
        }

        public void RemoveByID(int index)
        {
            try
            {
                List.RemoveAt(index);
                List[index] = null;
                Console.WriteLine($"The element index {index} is removed.");
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine($"There is no element with index: {index}", ConsoleColor.Red);
            }
        }

        public void ShowAll()
        {
            foreach (string a in List)
                Console.WriteLine(a, ConsoleColor.DarkBlue);
        }

        public void Inseert(int index, object ts)
        {
            List.Insert(index, ts.ToString());
        }

        public void InseertRange(int index, object[] ts)
        {
            for (int i = 0; i < ts.Length; i++)
            {
                List.Insert(index, ts[i].ToString());
                index++;

            }
        }
    }
}
