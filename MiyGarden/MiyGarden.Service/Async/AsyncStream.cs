using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiyGarden.Service.Async
{
    public class AsyncStream
    {
        public void Main()
        {
            const int count = 5;
            Console.WriteLine("Starting Async Streams Demo!");

            // Start a new task. Used to produce async sequence of data!
            IAsyncEnumerable<int> pullBasedAsyncSequence = ProduceAsyncSumSeqeunc(count).ToAsyncEnumerable();

            Console.WriteLine("X#X#X#X#X#X#X#X#X#X# Doing some other work X#X#X#X#X#X#X#X#X#X#");

            // Start another task; Used to consume the async data sequence!
            var consumingTask = Task.Run(() => ConsumeAsyncSumSeqeunc(pullBasedAsyncSequence));

            // Just for demo! Wait until the task is finished!
            consumingTask.Wait();
            Console.WriteLine("Async Streams Demo Done!");
        }

        private async Task ConsumeAsyncSumSeqeunc(IAsyncEnumerable<int> sequence)
        {
            Console.WriteLine("ConsumeAsyncSumSeqeunc Called");

            await sequence.ForEachAsync(value =>
            {
                Console.WriteLine($"Consuming the value: {value}");

                // simulate some delay!
                Task.Delay(TimeSpan.FromSeconds(1)).Wait();
            });
        }

        private IEnumerable<int> ProduceAsyncSumSeqeunc(int count)
        {
            Console.WriteLine("ProduceAsyncSumSeqeunc Called");
            var sum = 0;

            for (var i = 0; i <= count; i++)
            {
                sum = sum + i;

                // simulate some delay!
                Task.Delay(TimeSpan.FromSeconds(0.5)).Wait();

                yield return sum;
            }
        }
    }
}
