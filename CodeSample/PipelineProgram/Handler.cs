using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace CodeSample.PipelineProgram
{
    public class HandlerContext
    {
        public int Version { get; set; }
    }
    public delegate Task Handler(HandlerContext handlerContext);

    public delegate Handler Middleware(Handler handler);

    public static class Sample
    {
        public static void Case()
        {
            Func<Handler, Handler> first = h => c => { c.Version++; return h(c); };
            //after seed = handler3 : c => { c.Version++; return handler2(c); };
            Func<Handler, Handler> second = h => c => { c.Version += 10; return h(c); };
            //after seed = handler2 : c => { c.Version += 10; return handler1(c);
            Func<Handler, Handler> third = h => c => { c.Version += 20; return h(c); };
            //after seed = handler1 : c => { c.Version += 20; return seed(c); };
            IEnumerable<Func<Handler,Handler>> list = new List<Func<Handler, Handler>>()
            {
                first,
                second,
                third,
            };
            Handler seed = c => { Console.WriteLine(c.Version);return Task.CompletedTask; };
            first(seed)(new HandlerContext());
            //将中间件颠倒是为了使中间件的执行顺序与注册顺序一致。
            var app = list.Reverse().Aggregate(seed, (n, current) => current(n));
            app(new HandlerContext());

            Handler next = seed;
            foreach (var item in list.Reverse())
            {
                next = item(next); //此时并没有调用方法。而是把next变成了 c => { something; return next(c) }的函数；因此循环调用后，最初调用的那个函数将在最后调用。
            }
            next(new HandlerContext());

            IEnumerable<int> intList = new List<int>()
             {
                 1,2,3,4,5,6,7,8,9,10
             };
            var rst = intList.Aggregate("Hello", (n, current) => n += current);
            //等价于
            var preString = "Hello";
            foreach (var item in intList)
            {
                preString += item;
            }
            Console.WriteLine(preString);
        }

        static TResult Aggregate<T,TResult>(IEnumerable<T> list,TResult seed,Func<TResult,T,TResult> func)
        {
            foreach (var item in list)
            {
                func(seed, item);
            }
            return seed;
        }


        //public RequestDelegate Build()
        //{
        //    RequestDelegate seed = context => Task.Run(() => { });
        //    return middlewares.Reverse().Aggregate(seed, (next, current) => current(next));
        //}

        //public RequestDelegate BuildVersion2()
        //{
        //    RequestDelegate seed = context => Task.Run(() => { });
        //    RequestDelegate next = null;
        //    foreach (var item in middlewares.Reverse())
        //    {
        //        if (next == null)
        //        {
        //            next = item(seed);
        //        }
        //        next = item(next);
        //    }
        //    return next;
        //}
    }
}
