using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiyGarden.Service.Async
{
    public class TaskAsync
    {
        public void Start()
        {
            var tokenSource = new CancellationTokenSource();

            Task.Factory.StartNew(async () =>
            {
                do
                {
                    try
                    {
                        await this.DoSomeee(tokenSource.Token);
                    }
                    catch (Exception ex)
                    {
                        tokenSource.Cancel();
                    }
                } while (!tokenSource.IsCancellationRequested);
            });
        }

        private readonly BlockingCollection<MyTask>[] queues = new BlockingCollection<MyTask>[3 + 1]
        {
            null,
            new BlockingCollection<MyTask>(),
            new BlockingCollection<MyTask>(),
            new BlockingCollection<MyTask>(),
        };

        private int[] processTasks;

        public void StartThread(int total)
        {
            _total = total;
            //this.Foreach(tasks);
            //this.Plinq1(this.GetTasks());
            //this.Plinq2(this.GetTasks());
            //this.Plinq3(this.GetTasks());
            //this.Pipeline(this.GetTasks(), total);
            this.Pipeline2(this.GetTasks());
        }

        private int _total = 0;

        private IEnumerable<MyTask> GetTasks()
        {
            for (var i = 0; i < _total; i++)
                yield return new MyTask() { Id = i };
        }

        private void Foreach(IEnumerable<MyTask> tasks)
        {
            var ss = new Stopwatch();
            ss.Start();
            Console.WriteLine("Foreach start");
            foreach (var task in tasks)
            {
                task.Do(1);
                task.Do(2);
                task.Do(3);
            }
            Console.WriteLine("Foreach end" + ss.ElapsedMilliseconds);
        }

        private void Plinq1(IEnumerable<MyTask> tasks)
        {
            var ss = new Stopwatch();
            Task.Run(() =>
            {
                ss.Start();
                Console.WriteLine("Plinq1 start");
                tasks.AsParallel()
                .WithDegreeOfParallelism(11)
                .ForAll((t) =>
                {
                    Task.Run(() => { t.Do(1); })
                    .ContinueWith((x) => { t.Do(2); })
                    .ContinueWith((x) => { t.Do(3); })
                    .Wait();
                });
            }).ContinueWith((x) => Console.WriteLine("Plinq1 end" + ss.ElapsedMilliseconds))
            .Wait();
        }

        private void Plinq2(IEnumerable<MyTask> tasks)
        {
            var ss = new Stopwatch();
            Task.Run(() =>
            {
                ss.Start();
                Console.WriteLine("Plinq2 start");
                tasks.AsParallel()
                .WithDegreeOfParallelism(11)
                .ForAll((t) =>
                   {
                       t.Do(1);
                       t.Do(2);
                       t.Do(3);
                   });
            }).ContinueWith((x) => Console.WriteLine("Plinq2 end" + ss.ElapsedMilliseconds))
            .Wait();
        }

        private void Plinq3(IEnumerable<MyTask> tasks)
        {
            var ss = new Stopwatch();
            ss.Start();
            Console.WriteLine("Plinq3 start");
            var a = Task.WhenAll(Partitioner.Create(tasks)
               .GetPartitions(Environment.ProcessorCount)
               .Select(partition => Task.Run(() =>
                {
                    using (partition)
                        while (partition.MoveNext())
                        {
                            if (partition.Current == null) continue;
                            partition.Current.Do(1);
                            partition.Current.Do(2);
                            partition.Current.Do(3);
                        }
                })));
            a.GetAwaiter().GetResult();
            Console.WriteLine("Plinq3 end" + ss.ElapsedMilliseconds);
        }

        private void Pipeline(IEnumerable<MyTask> tasks, int total)
        {
            processTasks = new int[] { 0, total, total, total };
            var counts = new int[] { 0, 5, 3, 3 };

            for (int step = 1; step <= 3; step++)
            {
                for (var count = 0; count < counts[step]; count++)
                    new Thread(this.DoAllStepN).Start(step);
            }

            foreach (var task in tasks) this.queues[1].Add(task);
        }

        private void Pipeline2(IEnumerable<MyTask> tasks)
        {
            foreach (var t in s3(s2(s1(tasks)))) ;

            IEnumerable<MyTask> s1(IEnumerable<MyTask> task)
            {
                var block = new BlockingCollection<MyTask>(1);
                Task.Run(() =>
                {
                    foreach (var t in task)
                    {
                        t.Do(1);
                        block.Add(t);
                    }
                    block.CompleteAdding();
                });

                return block.GetConsumingEnumerable();
            }

            IEnumerable<MyTask> s2(IEnumerable<MyTask> task)
            {
                var block = new BlockingCollection<MyTask>(1);
                Task.Run(() =>
                {
                    foreach (var t in task)
                    {
                        t.Do(2);
                        block.Add(t);
                    }
                    block.CompleteAdding();
                });

                return block.GetConsumingEnumerable();
            }

            IEnumerable<MyTask> s3(IEnumerable<MyTask> task)
            {
                var block = new BlockingCollection<MyTask>(1);
                Task.Run(() =>
                {
                    foreach (var t in task)
                    {
                        t.Do(3);
                        block.Add(t);
                    }
                    block.CompleteAdding();
                });

                return block.GetConsumingEnumerable();
            }
        }

        private void DoAllStepN(object step_value)
        {
            int step = (int)step_value;
            bool _last = (step == 3);

            foreach (var task in this.queues[step].GetConsumingEnumerable())
            {
                task.Do(step);
                if (!_last) this.queues[step + 1].Add(task);
                if (processTasks[step] > 1)
                    processTasks[step]--;
                else
                {
                    processTasks[step]--;
                    this.queues[step].CompleteAdding();
                }
            }
        }

        public class MyTask
        {
            public int Id { get; set; }

            public byte[] Buffer { get; set; } = new byte[1024];

            public void Do(int step)
            {
                Thread.Sleep(1000 * step);
                Console.WriteLine(this.Id + " do:" + step);
            }

            public MyTask()
            {
                Random rnd = new Random();
                rnd.NextBytes(this.Buffer);
            }
        }

        private async Task DoSomeee(CancellationToken token)
        {
            CancellationTokenSource receiveTimeout = new CancellationTokenSource(TimeSpan.FromSeconds(3));
            using (CancellationTokenSource linkedCts = CancellationTokenSource.CreateLinkedTokenSource(token, receiveTimeout.Token))
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
                Console.WriteLine("MyTask 工作執行完畢。");
            }
        }
    }
}