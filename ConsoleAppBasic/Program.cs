using System;
using System.Reflection;
using System.Reflection.Emit;

namespace ConsoleAppBasic
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new Person("tim lv");
            var props = p.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

            var fields = p.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var field in fields)
            {
                field.SetValue(p, "haha");
                Console.WriteLine(field.Name);
            }
            Console.WriteLine(p);

            Console.ReadKey();
            Console.WriteLine("Hello World!");
        }
    }

    class Person
    {
        private string id;
        public Person(string Id)
        {
            id = Id;
        }
        public override string ToString()
        {
            return id;
        }
    }
}
