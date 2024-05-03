using IdGen;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace MiyGarden.Service.Algorithm
{
    public class IdGenTest
    {
        // 若同毫秒從GeneratorID(IP尾三碼相同)一樣的A切到B，由於Sequence重置則有可能會碰撞
        public void Main()
        {
            var epoch = new DateTime(2017, 7, 1, 0, 0, 0, DateTimeKind.Utc);
            var generatorID = GetGeneratorID(3);
            var gen = new IdGenerator(generatorID, epoch);
            Console.WriteLine("generatorID_10:" + generatorID);
            Console.WriteLine("generatorID_2:" + Convert.ToString(generatorID, 2));
            for (var i = 0; i < 100; i++)
            {
                var a = gen.CreateId();
                Console.WriteLine(a);
                Console.WriteLine(Convert.ToString(a, 2));
            }
            for (var i = 0; i < 100; i++)
            {
                Thread.Sleep(i);
                var a = gen.CreateId();
                Console.WriteLine(a);
                Console.WriteLine(Convert.ToString(a, 2));
            }
        }

        private static byte GetGeneratorID(int segmentLength)
        {
            var ipv4Address = Array.FindLast(Dns.GetHostEntry(Dns.GetHostName()).AddressList,
                address => address.AddressFamily == AddressFamily.InterNetwork);
            var lastSegment = ipv4Address.ToString().Split('.')[segmentLength];
            byte.TryParse(lastSegment, out var generatorID);

            return generatorID;
        }
    }
}
