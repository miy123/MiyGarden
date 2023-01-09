using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiyGarden.Service.Async
{
    public class TaskCancel
    {
        public void Main()
        {
            // Define the cancellation token.
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken token = source.Token;

            Random rnd = new Random();
            Object lockObj = new Object();

            List<Task<int[]>> tasks = new List<Task<int[]>>();
            TaskFactory factory = new TaskFactory(token);
            for (int taskCtr = 0; taskCtr <= 10; taskCtr++)
            {
                int iteration = taskCtr + 1;
                tasks.Add(factory.StartNew(() =>
                {
                    int value;
                    int[] values = new int[10];
                    for (int ctr = 1; ctr <= 10; ctr++)
                    {
                        lock (lockObj)
                        {
                            value = rnd.Next(0, 101);
                        }
                        if (value == 0)
                        {
                            source.Cancel();
                            Console.WriteLine("Cancelling at task {0}", iteration);
                            break;
                        }
                        values[ctr - 1] = value;
                    }
                    return values;
                }, token));
            }
            try
            {
                Task<double> fTask = factory.ContinueWhenAll(tasks.ToArray(),
                                                             (results) =>
                                                             {
                                                                 Console.WriteLine("Calculating overall mean...");
                                                                 long sum = 0;
                                                                 int n = 0;
                                                                 foreach (var t in results)
                                                                 {
                                                                     foreach (var r in t.Result)
                                                                     {
                                                                         sum += r;
                                                                         n++;
                                                                     }
                                                                 }
                                                                 return sum / (double)n;
                                                             }, token);
                Console.WriteLine("The mean is {0}.", fTask.Result);
            }
            catch (AggregateException ae)
            {
                foreach (Exception e in ae.InnerExceptions)
                {
                    if (e is TaskCanceledException)
                        Console.WriteLine("Unable to compute mean: {0}",
                                          ((TaskCanceledException)e).Message);
                    else
                        Console.WriteLine("Exception: " + e.GetType().Name);
                }
            }
            finally
            {
                source.Dispose();
            }
        }

        public void Main2()
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;
            var task = Task.Run(() =>
            {
                for (var i = 0; i < 10000; i++)
                {
                    if (token.IsCancellationRequested)
                    {
                        Console.WriteLine("can");
                        token.ThrowIfCancellationRequested();
                    }
                    Console.WriteLine(i + ":times.");
                }
            }, token);
            cts.CancelAfter(100);

            try
            {
                task.Wait();
                Console.WriteLine("fin.");
            }
            catch (AggregateException ex)
            {
                foreach (Exception e in ex.InnerExceptions)
                {
                    if (e is TaskCanceledException eee)
                        Console.WriteLine("ex," + eee.CancellationToken.IsCancellationRequested);
                    else
                        Console.WriteLine("Exception: " + e.GetType().Name);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("3:Exception: " + ex.GetType().Name);
            }
        }

        public void Main3()
        {
            int[] source = Enumerable.Range(1, 10000000).ToArray();
            using CancellationTokenSource cts = new CancellationTokenSource();

            // Start a new asynchronous task that will cancel the
            // operation from another thread. Typically you would call
            // Cancel() in response to a button click or some other
            // user interface event.
            Task.Factory.StartNew(() =>
            {
                UserClicksTheCancelButton(cts);
            });

            int[] results = null;
            try
            {
                results =
                    (from num in source.AsParallel().WithCancellation(cts.Token)
                     where num % 3 == 0
                     orderby num descending
                     select num).ToArray();
            }
            catch (OperationCanceledException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (AggregateException ae)
            {
                if (ae.InnerExceptions != null)
                {
                    foreach (Exception e in ae.InnerExceptions)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }

            foreach (var item in results ?? Array.Empty<int>())
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
            Console.ReadKey();
        }

        static void UserClicksTheCancelButton(CancellationTokenSource cts)
        {
            // Wait between 150 and 500 ms, then cancel.
            // Adjust these values if necessary to make
            // cancellation fire while query is still executing.
            Random rand = new Random();
            Thread.Sleep(rand.Next(150, 500));
            cts.Cancel();
        }
    }
}