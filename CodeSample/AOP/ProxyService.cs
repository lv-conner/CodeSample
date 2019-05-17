using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace CodeSample.AOP
{
    public class ProxyService : ISayHello
    {
        public void SayHello(string name)
        {
            AspectContext context = new AspectContext();
            List<Func<AspectDelegate, AspectDelegate>> interceptors = new List<Func<AspectDelegate, AspectDelegate>>();
            AspectDelegate seed = c => { c.ImplementMethod.Invoke(c.ImplementInstance, c.Paramenters.ToArray()); return Task.CompletedTask; };
            Func<AspectDelegate, AspectDelegate> interceptor = next => c => (c.ServiceProvider.GetService(typeof(LogInterceptor)) as LogInterceptor).Invoke(context, next);
            var app = interceptor(seed);
            app(context);
            //return context.result;
        }
    }
    public class CNSayHello : ISayHello
    {
        public void SayHello(string name)
        {
            Console.WriteLine("Hello");
        }
    }
    public interface ISayHello
    {
        [Interceptor(typeof(LogInterceptor))]
        void SayHello(string name);
    }
    public class AspectContext
    {
        public IServiceProvider ServiceProvider { get; set; } 
        public object ImplementInstance { get; set; }
        public MethodInfo ImplementMethod { get; set; }
        public object ProxyObject { get; set; }
        public MethodInfo ProxyMethod { get; set; }
        public IEnumerable<object> Paramenters { get; set; }
    }
    public delegate Task AspectDelegate(AspectContext context);
    public interface IInterceptor
    {
        Task Invoke(AspectContext context, AspectDelegate next);
    }
    public class LogInterceptor : IInterceptor
    {
        public async Task Invoke(AspectContext context, AspectDelegate next)
        {
            Console.WriteLine("begin");
            await next(context);
            Console.WriteLine("end");
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Method)]
    public class InterceptorAttribute:Attribute
    {
        public Type InterceptorType { get; private set; }
        public InterceptorAttribute(Type interceptorType)
        {
            InterceptorType = interceptorType;
        }
    }
}
