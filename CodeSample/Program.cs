using System;
using CodeSample.ExpressionTree;
using CodeSample.ExpressionTree.Test;

namespace CodeSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Tree.Convert<Person, PersonDto>(new Person());
            Console.ReadKey();
            Console.WriteLine("Hello World!");
        }
    }
}
