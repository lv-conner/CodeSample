using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using AspectCore.Extensions.Reflection;
using CodeSample.ExpressionTree;
using CodeSample.ExpressionTree.Test;
using CodeSample.PipelineProgram;
using System.Linq;

namespace CodeSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var ctinfo = typeof(Person).GetConstructor(new Type[] { });
            var constructorInfo = typeof(Person).GetTypeInfo().GetConstructor(new Type[0]);
            var reflector = constructorInfo.GetReflector();
            
            var aa = new object[] { };
            List<TimeSpan> times = new List<TimeSpan>();
            //Tree.Convert<Person, Pnew object[] { }ersonDto>(new Person());
            for (int i = 0; i < 1000000; i++)
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                var instance = ctinfo.Invoke(aa);
                //var instance = reflector.Invoke(aa);
                //var instance = new Person();
                stopwatch.Stop();
                //Console.WriteLine(stopwatch.Elapsed);
                times.Add(stopwatch.Elapsed);
            }

            Console.WriteLine(times.Average(p => p.Ticks));


            //Sample.Case();
            Console.ReadKey();
            Console.WriteLine("Hello World!");
        }
    }
}
