using System;
using System.Collections;
using System.Collections.Generic;
using Autofac;
using Autofac.Core;
using Autofac.Util;

namespace AutoFactSample
{
    public static class Program
    {
        static void Main(string[] args)
        {
            ContainerBuilder builer = new ContainerBuilder();
            builer.RegisterType<QQCar>().As<ICar>();
            builer.RegisterType<JCar>().As<ICar>();
            builer.RegisterType<CNName>().As<IName>();
            var di = builer.Build();

            var car = di.Resolve<IEnumerable<ICar>>();

            using (var scope = di.BeginLifetimeScope())
            {
                var car1 = scope.Resolve<ICar>();
            }
            Console.WriteLine(car.GetType().FullName);
            Console.WriteLine("auto fac");
            Console.ReadKey();
        }



    }

    public interface IName
    {
        
    }
    public class CNName:IName
    {

    }

    public interface ICar
    {

    }

    public class QQCar:ICar
    {
        public QQCar(IName name)
        {

        }
    }

    public class JCar:ICar
    {

    }
}
