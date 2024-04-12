using MiyGarden.Service.Linq;
using MiyGarden.Service.Others;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MiyGarden.WorkSpace
{
    static class Program
    {
        static void Main(string[] args)
        {
            //new BtreeValidator().Start2();
            //byte aaaa = 0;
            //object parameter = aaaa;
            //var a = parameter.GetType();
            //var b = a.Name;
            //var c = b.IndexOf("Int");
            //if (c != -1)
            //{

            //}

            //var str = "{\"Name\":\"clark\",\"MediaType\":2,\"MediaType123\":2}";
            //var model = JsonConvert.DeserializeObject<Person>(str);
            //Console.WriteLine(model.MediaType);
            //Console.WriteLine(new LDAPSvc().AuthAD_LdapConnection());
            //new TaskCancel().Main3();
            //string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //Task.Factory.StartNew(() =>
            //{
            //    Thread.Sleep(3000);
            //    using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "WriteLines.txt"), true))
            //    {
            //        outputFile.WriteLine("inner:" + Thread.CurrentThread.ManagedThreadId + DateTime.Now + "IsBackground:" + Thread.CurrentThread.IsBackground + ",IsThreadPoolThread:" + Thread.CurrentThread.IsThreadPoolThread);
            //    }
            //});
            //using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "WriteLines.txt"), true))
            //{
            //    outputFile.WriteLine("out:" + Thread.CurrentThread.ManagedThreadId + DateTime.Now + "IsBackground:" + Thread.CurrentThread.IsBackground + ",IsThreadPoolThread:" + Thread.CurrentThread.IsThreadPoolThread);
            //}
            //new BaccaratProcess().Main();
            // await new TaskTest().StartTest();
            //new Kmp().Main();
            //new ExpressionTest().LambdaExpressionTestExecute();
            //new ObserverPatternTest().Start();
            //new Jx3().Start();
            //StartTest();
            //StartForLoop();
            //StartGame();
            //StartCrawler();
            //StartEfPerformanceTest();
            //new LockTest().Main();
            //CollectionTest();
            //new StreamTest().CreateOrWrite();
            //new DecroratorPattern().StartTest();
            //StartFileStream();
            new Zodiac().Main();
        }

        private static void CollectionTest()
        {
            // a = b?.a => a 會重新被賦值 
            var arry1D = new int[] { 1, 2, 3, 4 };
            var arry1D2 = new int[] { 4, 7, 8, 9 };
            var arry2D = new int[4, 2];
            var arryMD = new int[][] { arry1D, arry1D2 };
            Stack<string> numbers = new Stack<string>();
            var hasSet = new HashSet<int>();
            var list = new List<int> { 1, 2, 3, 1, 5, 4, 4, 1 };
            Show(list);
            foreach (var item in arry1D.Union(arry1D2))
                hasSet.Add(item);
            hasSet.ExceptWith(arry1D);
            hasSet.RemoveWhere(x => x == 1);
            list.RemoveAll(x => x == 1);
            list.Sort((x, y) =>
            {
                if (x == y) return 0;
                else if (x > y) return 1;
                else return -1;
            });
            Show(hasSet);
            Show(list);
        }
        private static void StartTest()
        {
            var numbers = new int[] { 1, 2, 3, 4, 5, 6 };
            Show(numbers.Where(n => n > 3));
            Console.WriteLine(numbers.Aggregate((start, next) => start + next) + " " + numbers.Sum());
            new ExpressionTest().Excute();

            Console.ReadKey();
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

        /// <summary>
        /// 測試檔案類型之hex簽名
        /// </summary>
        private static void StartFileStream()
        {
            string[] paths = { @"C:\Users\kamis\Pictures\images.jpg", @"C:\Users\kamis\Pictures\images.pdf", @"C:\Users\kamis\Pictures\images.xlsx" };
            //string mimeType = MimeMapping.GetMimeMapping(paths[2]);

            List<string> result = new List<string>();
            foreach (var path in paths)
            {
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    int hexIn;
                    var hex = new StringBuilder();

                    while ((hexIn = fs.ReadByte()) != -1)
                    {
                        hex.Append(string.Format("{0:X2}", hexIn));
                    }

                    result.Add(hex.ToString());
                }
            }
        }
    }
}
