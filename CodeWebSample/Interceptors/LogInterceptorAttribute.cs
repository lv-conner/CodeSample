using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspectCore.DynamicProxy;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace CodeWebSample.Interceptors
{
    [AttributeUsage(AttributeTargets.Method)]
    public class LogInterceptorAttribute : AbstractInterceptorAttribute
    {
        public override bool AllowMultiple => false;
        public async override Task Invoke(AspectContext context, AspectDelegate next)
        {
            ILogger logger = context.ServiceProvider.GetService<ILoggerFactory>().CreateLogger<LogInterceptorAttribute>();
            logger.LogInformation("start",null);
            await next(context);
            logger.LogInformation("start",null);
        }
    }
}
