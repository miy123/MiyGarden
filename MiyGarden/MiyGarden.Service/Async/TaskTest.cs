using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiyGarden.Service.Async
{
    public class TaskTest
    {
        public async Task StartTest()
        {
            ShowThread();
            var wait = OutTask();
            SyncCode();
            await wait;
            ShowThread();
            Console.WriteLine("all done.");
        }

        private async Task OutTask()
        {
            Task wait = AsyncTask();
            Console.WriteLine("OutTask");
            await wait;
        }

        private async Task AsyncTask()
        {
            for (var i = 0; i < 3; i++)
            {
                ShowThread();
                DateTime start = DateTime.Now;
                Console.WriteLine("async: Starting");
                Task wait = Task.Delay(5000);
                Console.WriteLine("async: Running for {0} seconds", DateTime.Now.Subtract(start).TotalSeconds);
                await wait;
                Console.WriteLine("async: Running for {0} seconds", DateTime.Now.Subtract(start).TotalSeconds);
                Console.WriteLine("async: Done");
                ShowThread();
            }
        }

        private void SyncCode()
        {
            DateTime start = DateTime.Now;
            Console.WriteLine("sync: Starting");
            Thread.Sleep(5000);
            Console.WriteLine("sync: Running for {0} seconds", DateTime.Now.Subtract(start).TotalSeconds);
            Console.WriteLine("sync: Done");
        }

        private void ShowThread()
        {
            Console.WriteLine("this thread is #" + Thread.CurrentThread.ManagedThreadId);
        }

        private Task JustTask()
        {
            var cancellationToken = new CancellationToken();
            return new Task(ShowThread, cancellationToken, TaskCreationOptions.None);
        }
    }
}
