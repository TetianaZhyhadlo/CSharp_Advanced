using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Newtonsoft.Json;

using static ITEA_Collections.Common.Extensions;

namespace IteaSerialization
{
    [System.Runtime.InteropServices.Guid("01FDEB4A-7B33-45DD-B2A4-18B5F1DEA96E")]
    class Program
    {
        static void Main(string[] args)
        {
            //    ReadFromFile("example.txt");
            //    WriteToFile("example1.txt", "Some data");
            //    AppendToFile("example1.txt", "1");
            //    ToConsole(ReadFromFile("example.txt", ""));
            
            List<Person> people = new List<Person>
            {
                new Person("Pol", Gender.Man, 37, "pol@gmail.com"),
                new Person("Ann", Gender.Woman, 25, "ann@yahoo.com"),
                new Person("Alex", Gender.Man, 21, "alex@gmail.com"),
                new Person("Harry", Gender.Man, 58, "harry@yahoo.com"),
                new Person("Germiona", Gender.Woman, 18, "germiona@gmail.com"),
                new Person("Ron", Gender.Man, 24, "ron@yahoo.com"),
                new Person("Max", Gender.Man, 54, "max@ukr.net"),
                new Person("Nevil", Gender.Man, 19, "nevil@gmail.com"),
                new Person("Snake", Gender.etc, 40, "snake@yahoo.com"),
                new Person("Voldemar", Gender.etc, 38, "voldemar@spirit.net"),
            };
            Company epam = new Company("Epam");
            Department support = new Department("Support");
            Department developers = new Department("Developers");
            Department qa = new Department("QA");
            List<Department> departments = new List<Department>
            {
                 support,
                 developers,
                 qa,
            };
            departments.ForEach(x => x.SetCompanyForDepartment(epam));
            
            people.ForEach(x => {
                if (x.Age < 40 && x.Age >= 25)
                    x.SetDepartment(developers);
                else if (x.Age < 25)
                    x.SetDepartment(support);
                else
                    x.SetDepartment(qa);
            }) ;
            //people.ForEach(x => x.SetCompany());  

            //XmlSerialize("epamXml", people);
            JsonSerialize("epamJson", epam);

            Company epamFromFile = JsonDeserialize("epamJson");

            if (epam.Equals(epamFromFile))
                ToConsole($"Object {epam.Name} the same as after serialization.", ConsoleColor.Blue);
            else
                ToConsole($"Object {epam.Name} is different.", ConsoleColor.Red);

        }

        #region Serialization
        public static void XmlSerialize<T>(string path, T obj) where T : class
        {
            XmlSerializer formatter = new XmlSerializer(typeof(T));
            using (var fs = new FileStream($"{path}.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, obj);
            }

            using (var stringwriter = new StringWriter())
            {
                formatter.Serialize(stringwriter, obj);
                ToConsole(stringwriter.ToString());
            }
        }

        public static void JsonSerialize<T>(string path, T obj) where T : class
        {
            using (var fs = new FileStream($"{path}.json", FileMode.OpenOrCreate))
            {
                string strObj = JsonConvert.SerializeObject(obj);
                byte[] data = strObj
                    .Select(x => (byte)x)
                    .ToArray();
                fs.Write(data, 0, data.Length);
                strObj
                    .Split(",")
                    .ToList()
                    .ForEach(x => ToConsole($"{x},", ConsoleColor.Green));
            }
        }

        public static Company JsonDeserialize(string path)
        {
            using (var streamReader = new StreamReader($"{path}.json"))
            {
                //var startMemory = GC.GetTotalMemory(true);
                string dataStr = streamReader.ReadToEnd();
                return JsonConvert.DeserializeObject<Company>(dataStr);
                //var endMemory = GC.GetTotalMemory(true);
                //Console.WriteLine($"Total memory: {endMemory - startMemory}");
            }
        }
        #endregion
        #region System.IO
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path">Path to file</param>
        public static void ReadFromFile(string path)
        {
            using (var streamReader = new StreamReader(path))
            {
                var startMemory = GC.GetTotalMemory(true);
                streamReader
                    .ReadToEnd()
                    .Split(';')
                    .ShowAll(separator: ";")
                    .Select(x => long.TryParse(x, out long l) ? l : (long?)null)
                    .Where(x => x != null)
                    .ShowAll(separator: ";");
                var endMemory = GC.GetTotalMemory(true);
                Console.WriteLine($"Total memory: {endMemory - startMemory}");
            }
        }

        public static void WriteToFile(string path, string data)
        {
            using (var fileWriter = new FileStream(path, FileMode.OpenOrCreate))
            {
                byte[] array = data.Select(x => (byte)x).ToArray();
                fileWriter.Write(array, 0, array.Length);
            }

            //{
            //    FileStream fileWriter = new FileStream(path, FileMode.OpenOrCreate);
            //    try
            //    {
            //        byte[] array = data.Select(x => (byte)x).ToArray();
            //        fileWriter.Write(array, 0, array.Length);
            //    }
            //    finally
            //    {
            //        fileWriter?.Dispose();
            //    }
            //}

            using (var streamWriter = new StreamWriter(path))
            {
                streamWriter.WriteLine(data);
            }
        }

        public static void AppendToFile(string path, string data)
        {
            using (var fileWriter = new FileStream(path, FileMode.OpenOrCreate))
            {
                byte[] array = data.Select(x => (byte)x).ToArray();
                long fileDataLength = fileWriter.Length;
                fileWriter.Seek(fileDataLength, SeekOrigin.Begin);
                //fileWriter.Seek(0, SeekOrigin.End);
                fileWriter.Write(array, 0, array.Length);
            }
            using (var fileWriter = new FileStream(path, FileMode.Append))
            {
                byte[] array = data.Select(x => (byte)x).ToArray();
                fileWriter.Write(array, 0, array.Length);
            }
        }

        public static string ReadFromFile(string path, string notExistingEx)
        {
            notExistingEx = string.IsNullOrEmpty(notExistingEx)
                ? "Create file!"
                : notExistingEx;
            try
            {
                using (var fileReader = new FileStream(path, FileMode.Open))
                {
                    byte[] data = new byte[fileReader.Length];
                    fileReader.Read(data, 0, (int)fileReader.Length);
                    //return string.Concat(data.Select(x => (char)x));
                    return Encoding.Default.GetString(data);
                }
            }
            catch (FileNotFoundException)
            {
                ToConsole(notExistingEx, ConsoleColor.Red);
                return "Error";
            }
        }
        #endregion
    }
}
