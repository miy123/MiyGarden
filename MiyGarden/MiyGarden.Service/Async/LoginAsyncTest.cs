using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiyGarden.Service.Async
{
    public class LoginAsyncTest
    {
        public void Main()
        {
            var startTime = DateTime.Now.ToString("HH:mm:ss");
            var task = LogAsync();
            var endTime = DateTime.Now.ToString("HH:mm:ss");

            Console.WriteLine(String.Format("{0} -- {1}", startTime, endTime));
            Task.WaitAll(task);
        }

        private async Task LogAsync()
        {
            await Task.Delay(2000); // 刻意延遲九秒.
            System.IO.File.WriteAllText(@"D:\LogAsyncTest.txt",
                DateTime.Now.ToString("HH:mm:ss"));
        }
    }
}
