using MiyGarden.Service.Async;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MiyGarden.Async
{
    static class Program
    {
        static async Task Main(string[] args)
        {
            new LoginAsyncTest().Main();
            new TaskAsync().StartThread(5);
            new YieldTest().Test3();
            await StartStreamTestAsync();
        }

        private static async Task StartStreamTestAsync()
        {
            ShowMessage("Main threadId is:", false);

            var tasks = new List<Task>();
            CancellationTokenSource cts = new CancellationTokenSource();

            #region 非同步線程池 (ThreadPool -> Task.Run)
            for (var i = 0; i < 100; i++)
            {
                string taskName = $"PoolAsync({i})";
                tasks.Add(Task.Run(() =>
                {
                    ShowMessage(taskName + " threadId is:", false);
                    ShowMessage(taskName + " thread close");
                }));
            }
            #endregion

            #region 非同步線程 (Thread -> Task with CancellationToken)
            tasks.Add(Task.Run(async () =>
            {
                try
                {
                    ShowMessage("Async threadId is:");
                    for (int n = 0; n < 10; n++)
                    {
                        if (n >= 4)
                        {
                            cts.Cancel(); // 取消請求
                        }

                        if (cts.Token.IsCancellationRequested)
                        {
                            Console.WriteLine($"Thread abort when the number is: {n}!");
                            break;
                        }

                        Console.WriteLine("The number is:" + n);
                        await Task.Delay(500); // 模擬工作
                    }
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Async Thread Canceled!");
                }
                finally
                {
                    Console.WriteLine("Async Thread Close!");
                }
            }, cts.Token));
            #endregion

            await Task.WhenAll(tasks); // 等待所有任務完成
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
