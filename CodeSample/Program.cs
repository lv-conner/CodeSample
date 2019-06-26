using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using AspectCore.Extensions.Reflection;
using CodeSample.ExpressionTree;
using CodeSample.ExpressionTree.Test;
using CodeSample.PipelineProgram;
using System.Linq;
using CodeSample.LockSample;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;
using System.Threading;

namespace CodeSample
{
    class Program
    {
        static void Main(string[] args)
        {
            //var ctinfo = typeof(Person).GetConstructor(new Type[] { });
            //var constructorInfo = typeof(Person).GetTypeInfo().GetConstructor(new Type[0]);
            //var reflector = constructorInfo.GetReflector();

            //var aa = new object[] { };
            //List<TimeSpan> times = new List<TimeSpan>();
            ////Tree.Convert<Person, Pnew object[] { }ersonDto>(new Person());
            //for (int i = 0; i < 1000000; i++)
            //{
            //    Stopwatch stopwatch = new Stopwatch();
            //    stopwatch.Start();
            //    var instance = ctinfo.Invoke(aa);
            //    //var instance = reflector.Invoke(aa);
            //    //var instance = new Person();
            //    stopwatch.Stop();
            //    //Console.WriteLine(stopwatch.Elapsed);
            //    times.Add(stopwatch.Elapsed);
            //}
            Parallel.Invoke(() => Console.WriteLine("Hello"));
            //Console.WriteLine(times.Average(p => p.Ticks));
            SpinLockTest.Case();
            SpinLockTest.Case1();

            //token.RegisterChangeCallback(o => Console.WriteLine(o), "333");
            CancellationTokenSource source = new CancellationTokenSource();
            ChangeToken.OnChange(() => 
            {
                Console.WriteLine("Call");
                return new ConfigurationReloadToken();
            }, () =>
             {
                 Console.WriteLine("Change");
             });
            //Task.Run(() =>
            //{
            //    source.Cancel();
            //}); 
            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
    public class SimpleChangeTokenSource
    {
        public bool IsCancel { get; private set; } = false;
        public void Cancel()
        {
            IsCancel = true;
        }
        private SimpleChangeToken _token;
        public SimpleChangeToken Token
        {
            get
            {
                if(_token == null)
                {
                    _token = new SimpleChangeToken(this);
                }
                return _token;
            }
        }
    }
    public class SimpleChangeToken : IChangeToken
    {
        private SimpleChangeTokenSource _source;
        public SimpleChangeToken(SimpleChangeTokenSource source)
        {
            _source = source;
        }
        public bool HasChanged => _source.IsCancel;

        public bool ActiveChangeCallbacks => true;

        public IDisposable RegisterChangeCallback(Action<object> callback, object state)
        {
            throw new NotImplementedException();
        }
    }

    public class ConfigurationReloadToken : IChangeToken
    {
        private CancellationTokenSource _cts = new CancellationTokenSource();

        /// <summary>
        /// Indicates if this token will proactively raise callbacks. Callbacks are still guaranteed to be invoked, eventually.
        /// </summary>
        public bool ActiveChangeCallbacks => true;

        /// <summary>
        /// Gets a value that indicates if a change has occurred.
        /// </summary>
        public bool HasChanged => _cts.IsCancellationRequested;

        /// <summary>
        /// Registers for a callback that will be invoked when the entry has changed. <see cref="Microsoft.Extensions.Primitives.IChangeToken.HasChanged"/>
        /// MUST be set before the callback is invoked.
        /// </summary>
        /// <param name="callback">The callback to invoke.</param>
        /// <param name="state">State to be passed into the callback.</param>
        /// <returns></returns>
        public IDisposable RegisterChangeCallback(Action<object> callback, object state) => _cts.Token.Register(callback, state);

        /// <summary>
        /// Used to trigger the change token when a reload occurs.
        /// </summary>
        public void OnReload() => _cts.Cancel();
    }
}
