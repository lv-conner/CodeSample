using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeSample.LockSample
{
    public class SpinLockTest
    {
        private static SimpleSpinLock _lock = new SimpleSpinLock();
        public static void Case()
        {
            int rst = 0;
            var t1 = Task.Run(() => AddOperation(ref rst));
            var t2 = Task.Run(() => AddOperation(ref rst));
            Task.WaitAll(t1, t2);
            Console.WriteLine(rst);
        }
        public static void Case1()
        {
            int rst = 0;
            var t1 = Task.Run(() => AddOperationWithLock(ref rst));
            var t2 = Task.Run(() => AddOperationWithLock(ref rst));
            Task.WaitAll(t1, t2);
            Console.WriteLine(rst);
        }

        public static void AddOperation(ref int rst)
        {
            for (int i = 0; i < 1000; i++)
            {
                rst += i;
            }
        }
        public static void AddOperationWithLock(ref int rst)
        {
            for (int i = 0; i < 1000; i++)
            {
                _lock.Enter();
                rst += i;
                _lock.Leave();
            }
        }
    }
}
