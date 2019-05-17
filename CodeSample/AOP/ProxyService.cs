using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeSample.AOP
{
    public class ProxyService : ISayHello
    {
        public void SayHello(string name)
        {
            var context = new InterceptroContext();

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
    public class InterceptroContext
    {

    }
    public interface IInterceptor
    {
        Task Invoke(InterceptroContext context, Func<InterceptroContext,Task> next);
    }
    public class LogInterceptor : IInterceptor
    {
        public async Task Invoke(InterceptroContext context, Func<InterceptroContext,Task> next)
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
