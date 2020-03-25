using System;
using System.Collections.Generic;

using ITEA_Collections.Common;

namespace ITEA_Collections.Usings
{
    public class DictionaryUsing : IBaseCollectionUsing
    {
        public Dictionary<int, string> Dictionary { get; set; }

        public DictionaryUsing()
        {
            Dictionary = new Dictionary<int, string>();
        }

        public void Add(object ts)
        {
            Dictionary.Add(Dictionary.Count + 1, ts.ToString());
        }

        public void AddMany(object[] ts)
        {
            if (ts is null)
                Console.WriteLine($"Your array is null!", ConsoleColor.Red);
            else
            {
                for (int i = 0; i < ts.Length; i++)
                {
                    Dictionary.Add(Dictionary.Count + 1, ts[i].ToString());
                }

            }
        }

        public void Clear()
        {
            Dictionary.Clear();
        }

        public object[] GetAll()
        {
            object[] all = new object[Dictionary.Count * 2];
            int i = 0;
            foreach (KeyValuePair<int, string> element in Dictionary)
            {
                all[i] = element.Key;
                i++;
                all[i] = element.Value;
                i++;

            }
            return all;
        }

        public object GetByID(int index)
        {

            try
            {
                return Dictionary[index];
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
                Dictionary.Remove(index);
                Console.WriteLine($"The element index {index} is removed.");
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong", ConsoleColor.Red);
            }
        }

        public void ShowAll()
        {
            foreach (KeyValuePair<int, string> element in Dictionary)
            {
                Console.WriteLine($"Key:- {element.Key} and Value:- {element.Value}");
            }
        }
    }
}
