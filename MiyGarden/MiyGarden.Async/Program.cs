using MiyGarden.Service.Async;
using System;
using System.Threading;

namespace MiyGarden.Async
{
    static class Program
    {
        static void Main(string[] args)
        {
            new LoginAsyncTest().Main();
            new TaskAsync().StartThread(5);
            new YieldTest().Test3();
            StartStreamTest();
        }

        private static void StartStreamTest()
        {
            //ThreadPool.SetMaxThreads(5, 5);
            ShowMessage("Main threadId is:", false);
            #region 非同步線程池
            for (var i = 0; i < 100; i++)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback((state) =>
                {
                    ShowMessage((string)state + " threadId is:", false);
                    ShowMessage((string)state + " thread close");
                }), "PoolAsync(" + i + ")");
            }
            #endregion
            #region 非同步線程
            Thread thread = new Thread(new ThreadStart(() =>
            {
                try
                {
                    ShowMessage("Async threadId is:");
                    for (int n = 0; n < 10; n++)
                    {
                        //当n等于4时，终止线程
                        if (n >= 4)
                        {
                            Thread.CurrentThread.Abort(n);
                        }
                        Console.WriteLine("The number is:" + n.ToString());
                    }
                }
                catch (ThreadAbortException ex)
                {
                    //输出终止线程时n的值
                    if (ex.ExceptionState != null)
                        Console.WriteLine(string.Format("Thread abort when the number is: {0}!",
                                                         ex.ExceptionState.ToString()));

                    //取消终止，继续执行线程
                    Thread.ResetAbort();
                    Console.WriteLine("Async Thread ResetAbort!");
                }
                //线程结束
                Console.WriteLine("Async Thread Close!");
            }))
            {
                IsBackground = true
            };
            //thread.Start();
            //thread.Join();
            #endregion
            //WaitHandle.WaitAll();
            ShowMessage("Main thread close");
        }

        private static void ShowMessage(string data, bool isDelay = true)
        {
            if (isDelay)
                Thread.Sleep(10000);
            string message = string.Format(data + " threadId is:{0}",
                Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine(message);
        }
    }
}
