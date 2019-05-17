using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeWebSample.Interceptors;

namespace CodeWebSample.Services
{
    public interface IRepository
    {
        [LogInterceptor]
        void Add(string name);
    }
}
