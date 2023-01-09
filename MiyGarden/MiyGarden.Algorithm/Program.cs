using MiyGarden.Service.Algorithm;
using System;
using System.Linq;

namespace MiyGarden.Algorithm
{
    static class Program
    {
        static void Main(string[] args)
        {
            //SearchAlgorithm();
            SortAlgorithm();
            //FibonacciSequence();
            //ClusterAlgorithm();
            //Geometric();
        }

        /// <summary>
        /// 搜尋演算法
        /// </summary>
        private static void SortAlgorithm()
        {
            Console.WriteLine("==========快速排序=========");
            Console.WriteLine(string.Join(',', new SortAlgorithm().QuickSort(new int[] { 5, 3, 100, -4, 30, 40, 60, 22, 14, 6 })));
            Console.WriteLine("===========================");
        }

        /// <summary>
        /// 搜尋演算法
        /// </summary>
        private static void SearchAlgorithm()
        {
            Console.WriteLine("==========二元搜尋=========");
            new SearchAlgorithm().BinarySearch();
            Console.WriteLine("===========================");
        }

        /// <summary>
        /// 費式數列演算法
        /// </summary>
        private static void FibonacciSequence()
        {
            Console.WriteLine("==========費式數列=========");
            Console.WriteLine(new FibonacciSequence().GetNumber(5));
            Console.WriteLine("===========================");
        }

        /// <summary>
        /// 聚類演算法
        /// </summary>
        private static void ClusterAlgorithm()
        {
            Console.WriteLine("==========聚類演算=========");
            for (int i = 0; i < 20; i++)
            {
                new ClusterAlgorithm().KMeansPlus().ForEach(x =>
                {
                    Console.WriteLine(string.Join(",", x.Careers.Select(y => y.Name)));
                });
                Console.WriteLine("*****");
            }
            Console.WriteLine("===========================");
        }

        /// <summary>
        /// 菱形
        /// </summary>
        private static void Geometric()
        {
            var geometric = new Geometric();
            Console.WriteLine("============菱形===========");
            geometric.Rin(5);
            geometric.Rin2(5);
            Console.WriteLine("===========================");
        }
    }
}
