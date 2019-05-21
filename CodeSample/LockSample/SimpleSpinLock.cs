using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CodeSample.LockSample
{
    public class SimpleSpinLock
    {
        private int _lock = 0;
        public void Enter()
        {
            while(true)
            {
                //将_lock赋值为1，并返回原值，因此在_lock为0时，可以直接返回，否则在此自旋
                if(Interlocked.Exchange(ref _lock,1)== 0)
                {
                    return;
                }
            }
        }
        /// <summary>
        /// 释放锁，由于Volatile.Write方法的特性，读取该变量的值的操作都必须在这个写入操作完成之后才能进行。因此，在写入值后，等待线程将获取到锁，继续执行。
        /// </summary>
        public void Leave()
        {
            Volatile.Write(ref _lock, 0);
        }
    }
}
