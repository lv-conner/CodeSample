using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Polly;
using Polly.Caching;
using Polly.Caching.Memory;

namespace CodeSample.PollyCode
{
    public static class PollyCase
    {
        public static void Case1()
        {
            //限流
            Policy.Bulkhead(2, 2, context => { Console.Write(context.Count); }).Execute(() =>
              {
                  Console.WriteLine("Hello World");
              });
            //限流
            var s = Policy.Bulkhead<string>(2, 2, context =>
              {
                  Console.WriteLine(context.PolicyKey);
              }).Execute(() =>
              {
                  return "Hello world";
  
              });
            //限流
            var t = Policy.BulkheadAsync(2, 2, context =>
              {
                  return Task.CompletedTask;
              }).ExecuteAsync(() =>
              {
                  return Task.CompletedTask;
              });
            //限流
            var ts = Policy.BulkheadAsync<string>(2, 2, context =>
              {
                  return Task.CompletedTask;
              }).ExecuteAsync(() =>
              {
                  return Task.FromResult("Hello world");
              });
            var cache = new MemoryCache(null);
            var cacheProvider = new MemoryCacheProvider(cache);
            //缓存
            Policy.Cache(cacheProvider, TimeSpan.FromDays(1), (c, ss, e) => { }).Execute(() => { });
            Policy.Cache<string>(cacheProvider, TimeSpan.FromHours(2), (c, ss, e) => { }).Execute(() => "Hello");

            //熔断
            Policy.Handle<TimeoutException>(toe => true).CircuitBreaker(2, TimeSpan.FromMinutes(1)).Execute(() => throw new TimeoutException());
            //后备
            Policy.Handle<TimeoutException>(toe => true).Fallback(() => Console.WriteLine("call fallback"));
        }
    }
}
