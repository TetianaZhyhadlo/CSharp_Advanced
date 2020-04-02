using IteaDelegates.IteaMessanger;
using System;
using System.Collections.Generic;
using System.Linq;

using static ITEA_Collections.Common.Extensions;

namespace IteaLinq
{
    class Program
    {
        static void Main(string[] args)
        {
            //List<Person> people = GetPeople().ToList();

            //foreach (Person x in people)
            //{
            //    ToConsole(x.ToString(), ConsoleColor.Cyan);
            //}

            //people
            //    .ForEach(x => ToConsole(x.ToString(), ConsoleColor.Cyan));

            //people
            //    .CustomWhere(x => x.Age < 28)
            //    .ToList()
            //    .ForEach(x => ToConsole(x.ToString(), ConsoleColor.Cyan));

            //foreach (Person x in from i in people where i.Age < 28 select i)
            //    ToConsole(x.ToString(), ConsoleColor.Cyan);

            //IOrderedEnumerable<Person> ordered1 = people
            //    .Where(x => x.Age > 35)
            //    .OrderByDescending(x => x.Age);

            //var ordered2 = from i in people
            //               where i.Age > 35
            //               orderby i.Age descending
            //               select new { i.Name };


            //int min = people.Min(x => x.Age);
            //int max = people.Max(x => x.Age);
            //double avr = people.Average(x => x.Age);

            //var tenten = people.Skip(10).Take(10);

            //var anon = new
            //{
            //    Name = "Anon",
            //    Age = 21
            //};

            //var anon1 = new
            //{
            //    Name = "Anon",
            //    Age = "dwqd"
            //};

            //ToConsole(anon.Age.GetType().Name);
            //ToConsole(anon1.Age.GetType().Name);

            //List<Person> people = new List<Person>
            //{
            //    new Person("Pol", Gender.Man, 37, "pol@gmail.com"),
            //    new Person("Ann", Gender.Woman, 25, "ann@yahoo.com"),
            //    new Person("Alex", Gender.Man, 21, "alex@gmail.com"),
            //    new Person("Harry", Gender.Man, 58, "harry@yahoo.com"),
            //    new Person("Germiona", Gender.Woman, 18, "germiona@gmail.com"),
            //    new Person("Ron", Gender.Man, 24, "ron@yahoo.com"),
            //    new Person("Etc1", Gender.etc, 42, "etc1@yahoo.com"),
            //    new Person("Etc2", Gender.etc, 42, "etc2@gmail.com"),
            //};

            //people
            //    .CustomWhere(x => x.Email.Contains("gmail"))
            //    .ShowAll()
            //    .OrderByDescending(x => x.Age)
            //    .ShowAll();
            Account user1 = new Account("Kate");
            Account user2 = new Account("Ann");
            Account user3 = new Account("Lu");
            Account user4 = new Account("Mike");
            Account user5 = new Account("Alex");
            List<Account> userList = new List<Account>();
            List<Message> messagelist = new List<Message>();
            userList.Add(user1);
            userList.Add(user2);
            userList.Add(user3);
            userList.Add(user4);
            userList.Add(user5);
            messagelist.Add(user1.CreateMessage("I'm sick,sorry.", user3));
            messagelist.Add(user3.CreateMessage("Ok", user5));
            messagelist.Add(user1.CreateMessage("Hope to be at work soon", user3));
            messagelist.Add(user3.CreateMessage("I'll call you later", user1));
            messagelist.Add(user5.CreateMessage("Good morning", user3));
            messagelist.Add(user5.CreateMessage("I'll be late", user3));
            messagelist.Add(user3.CreateMessage("Good morning", user5));
            messagelist.Add(user2.CreateMessage("Call Mike", user5));
            messagelist.Add(user3.CreateMessage("Breakfast?", user5));
            messagelist.Add(user2.CreateMessage("Please", user5));
                       
            foreach (Account y in userList)
            {
                int count = messagelist
                    .Where(x => x.To.Username == y.Username || x.From.Username == y.Username)
                    .Count();
                ToConsole(count, ConsoleColor.Green);
            }

            foreach (Account y in userList)
            {
                
                int count = messagelist
                    .Where(x => x.To.Username == y.Username || x.From.Username == y.Username)
                    .Count();
                int max1 = 0;
                int max2 = 0;
                int max3 = 0;
                string acc1 = null;
                string acc2 = null;
                string acc3 = null;
                if (count > max1)
                {
                    max1 = count;
                    acc1 = y.Username;
                }
                else if (count > max2)
                {
                    max2 = count;
                    acc2 = y.Username;
                }
                else if (count > max3)
                {
                    max3 = count;
                    acc3 = y.Username;
                }
                ToConsole($"{max1},{acc1}\n{max2},{acc2}\n{max3},{acc3}", ConsoleColor.Green);
            }
            












        }

        #region Create people list
        public static IEnumerable<Person> GetPeople()
        {
            for (int i = 0; i < 20; i++)
            {
                yield return new Person("Person" + i, 18 + i * 2);
            }
        }
        #endregion

        static void BaseDelegates(int f, int s)
        {
            Action<int, int> action = (a, b) => Console.WriteLine($"{a}{b}");
            Predicate<int> predicate = (a) => a > 0;
            Func<int, int, string> func = delegate (int a, int b)
            {
                return (a * b).ToString();
            };
            action(f, s);
            predicate(f);
            func(f, s);
        }

    }
}
