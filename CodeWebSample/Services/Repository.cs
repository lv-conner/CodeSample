using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace CodeWebSample.Services
{
    public class Repository : IRepository
    {
        private readonly ILogger _logger;
        private readonly IMemoryCache _cache;
        public Repository(ILoggerFactory loggerFactory,IMemoryCache cache)
        {
            _logger = loggerFactory.CreateLogger<Repository>();
            _cache = cache;
        }
        public void Add(string name)
        {
            Console.WriteLine(name);
        }
    }
}
