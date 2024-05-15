using MiyGarden.Service.Interfaces;
using System;
using System.Collections.Generic;

namespace MiyGarden.Service.LeetCode
{
    /// <summary>
    /// Given an integer numRows, return the first numRows of Pascal's triangle.
    /// In Pascal's triangle, each number is the sum of the two numbers directly above it as shown:
    /// Example 1:
    /// Input: numRows = 5
    /// Output: [[1],[1,1],[1,2,1],[1,3,3,1],[1,4,6,4,1]]
    /// Example 2:
    /// Input: numRows = 1
    /// Output: [[1]]
    /// Constraints:
    /// 1 <= numRows <= 30
    /// </summary>
    public class _0118_Pascal_s_Triangle : ILeetCode
    {
        public int Number => 118;

        public string[] Main()
        {
            var result = Generate(2);
            foreach (var x in result)
                Console.WriteLine(string.Join(',', x));
            result = Generate(5);
            foreach (var x in result)
                Console.WriteLine(string.Join(',', x));
            return new string[0];
        }

        public IList<IList<int>> Generate(int numRows)
        {
            var result = new IList<int>[numRows];
            result[0] = new int[] { 1 };
            if (numRows == 1) return result;
            result[1] = new int[] { 1, 1 };
            if (numRows == 2) return result;
            for (var k = 3; k <= numRows; k++) // k = 深度
            {
                var list = new int[k];
                list[0] = 1;
                list[k - 1] = 1;
                for (var j = 1; j < k - 1; j++)
                {
                    list[j] = result[k - 2][j - 1] + result[k - 2][j]; // result[k - 2] = 上一層的陣列
                }
                result[k - 1] = list;
            }

            return result;
        }

        //public IList<IList<int>> Generate(int numRows)
        //{
        //    var result = new List<IList<int>>();
        //    for (var k = 1; k <= numRows; k++)
        //    {
        //        if (k == 1) result.Add(new List<int>() { 1 });
        //        else
        //        {
        //            var list = new List<int>();
        //            for (var j = 0; j < k; j++)
        //            {
        //                list.Add(Get(j, k));
        //            }
        //            result.Add(list);
        //        }
        //    }

        //    return result;
        //}

        //private int Get(int index, int k)
        //{
        //    if (k == 2 || index == 0 || index == k - 1) return 1;
        //    return Get(index - 1, k - 1) + Get(index, k - 1);
        //}
    }
}
