using MiyGarden.Service.Others;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace MiyGarden.WorkSpace
{
    static partial class Program
    {
        static void Main(string[] args)
        {
            TestGenerator.Hello("Generated Code");
            //new BtreeValidator().Start2();
            //Console.WriteLine(new LDAPSvc().AuthAD_LdapConnection());
            //new TaskCancel().Main3();
            // await new TaskTest().StartTest();
            //new Kmp().Main();
            //new ExpressionTest().LambdaExpressionTestExecute();
            //new ObserverPatternTest().Start();
            //new Jx3().Start();
            //new ExpressionTest().Excute();
            //StartForLoop();
            //StartGame();
            //StartCrawler();
            //StartEfPerformanceTest();
            //new LockTest().Main();
            //new StreamTest().CreateOrWrite();
            //new DecroratorPattern().StartTest();
            //new Zodiac().Main();
            //new BaccaratProcess().Main();
            //new IdGenTest().Main();

            //Console.WriteLine("MainO" + Thread.CurrentThread.ManagedThreadId);
            //Task.Run(async () =>
            //{
            //    Console.WriteLine("MainI" + Thread.CurrentThread.ManagedThreadId);
            //    var a = Tettt();
            //    for (var i = 0; i < 9999; i++)
            //    {
            //        Console.Write("Main" + i);
            //    }
            //    await a;
            //    Console.WriteLine("Main" + Thread.CurrentThread.ManagedThreadId);
            //}).Wait();
        }

        private static async Task Tettt()
        {
            Console.WriteLine("Tettt" + Thread.CurrentThread.ManagedThreadId);
            await Task.Delay(500);
            for (var i = 0; i < 2; i++)
            {
                Console.Write("Tettt" + i);
            }
            Console.WriteLine("Tettt" + Thread.CurrentThread.ManagedThreadId);
        }

        /// <summary>
        /// XAXB遊戲
        /// </summary>
        private static void StartGame()
        {
            Game game = new Game();
            game.StartGame();
            game.StartRound();
            Console.ReadKey();
        }

        private static void StartForLoop()
        {
            string key = Console.ReadLine();
            for (var index = 0; index < 100; index++)
            {
                for (var index2 = 0; index2 < index; index2++)
                    Console.Write(key);
                Console.WriteLine();
            }
            Console.ReadKey();
        }

        private static void StartCrawler()
        {
            var url = "https://forum.gamer.com.tw/B.php?bsn=16357";
            var list = new List<object>();
            var crawler = new Crawler();
            crawler.OnStart += (s, e) =>
            {
                Console.WriteLine("Start Uri" + e.Uri.ToString());
            };
            crawler.OnError += (s, e) =>
            {
                Console.WriteLine("Catch Error" + e.Message);
            };
            crawler.OnCompleted += (s, e) =>
            {
                var links = Regex.Matches(e.PageSource,
                    @"<a[^>]+href=""*(?<URL>[^""]*).*? class=""b-list__main__title""[^>]*>(?<text>[^<]+)</a>"
                    ,
                    RegexOptions.IgnoreCase | RegexOptions.Compiled);
                foreach (Match item in links)
                {
                    var name = item.Groups["text"].Value;
                    var uri = item.Groups["URL"].Value;
                    Console.WriteLine(name);
                    Console.WriteLine("https://forum.gamer.com.tw/" + uri);
                }
                //Console.WriteLine(e.PageSource);
                Console.WriteLine("===================");
                Console.WriteLine("耗時：" + e.MilliSeconds + "毫秒");
                Console.WriteLine("線程：" + e.ThreadId);
                Console.WriteLine("Uri：" + e.Uri);
            };
            crawler.Start(new Uri(url)).Wait();
            Console.ReadKey();
        }

        private static void StartEfPerformanceTest()
        {
            //var testService = new EFTest();
            //int count = 10000;
            //var timeWatch = new Stopwatch();
            //timeWatch.Start();
            //var result = testService.CreateTest(count);
            //var time = timeWatch.ElapsedMilliseconds;
            //timeWatch.Stop();
            //if (result.Success)
            //{
            //    Console.WriteLine("總計筆數：" + count + "筆");
            //    Console.WriteLine("總計耗時：" + time + "毫秒");
            //}
            //else
            //    Console.WriteLine(result.Message);
            //Console.ReadKey();
        }

        private static void Show<T>(IEnumerable<T> lists)
        {
            void _Show()
            {
                foreach (var item in lists)
                    Console.WriteLine(item);
                Console.WriteLine("----");
            }
            _Show();
        }
    }
}
