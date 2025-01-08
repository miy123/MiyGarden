using MiyGarden.Service.Interfaces;
using System.Collections.Generic;

namespace MiyGarden.Service.LeetCode
{
    /// <summary>
    /// Given an m x n matrix, return all elements of the matrix in spiral(螺旋) order.
    /// </summary>
    public class _0054_Spiral_Matrix : ILeetCode
    {
        public int Number => 54;

        public string[] Main()
        {
            //var re = SpiralOrder(new int[][] { new int[] { 1, 2, 3 }, new int[] { 4, 5, 6 }, new int[] { 7, 8, 9 } });
            var re = SpiralOrder(new int[][] { new int[] { 1, 2, 3, 4 }, new int[] { 5, 6, 7, 8 }, new int[] { 9, 10, 11, 12 } });
            return new string[] { };
        }

        public IList<int> SpiralOrder(int[][] matrix)
        {
            var len = matrix.Length * matrix[0].Length;
            var result = new List<int>(len);

            var loopTimes = 0;
            var rlLength = matrix[0].Length;
            var udLength = matrix.Length;
            var endJ = matrix[0].Length - 1;

            do
            {
                loopTimes++;
                var rr = rlLength - (loopTimes - 1) * 2;
                if (rr == 0) break;
                Right(result, matrix, loopTimes - 1, loopTimes - 1, rr);
                var dd = udLength + 1 - (loopTimes * 2);
                if (dd == 0) break;
                Down(result, matrix, endJ - loopTimes + 1, loopTimes, dd);
                var ll = rlLength - 1 - ((loopTimes - 1) * 2);
                if (ll == 0) break;
                Left(result, matrix, udLength - loopTimes, rlLength - 1 - loopTimes, ll);
                var uu = udLength - (loopTimes * 2);
                if (uu == 0) break;
                Up(result, matrix, loopTimes - 1, udLength - 1 - loopTimes, uu);
            } while (udLength > loopTimes * 2);

            return result;
        }

        private static void Right(List<int> result, int[][] matrix, int i, int startJ, int length)
        {
            for (var j = startJ; j < length + startJ; j++)
            {
                result.Add(matrix[i][j]);
            }
        }
        private static void Down(List<int> result, int[][] matrix, int j, int startI, int length)
        {
            for (var i = startI; i < length + startI; i++)
            {
                result.Add(matrix[i][j]);
            }
        }
        private static void Left(List<int> result, int[][] matrix, int i, int startJ, int length)
        {
            for (var j = startJ; j > startJ - length; j--)
            {
                result.Add(matrix[i][j]);
            }
        }
        private static void Up(List<int> result, int[][] matrix, int j, int startI, int length)
        {
            for (var i = startI; i > startI - length; i--)
            {
                result.Add(matrix[i][j]);
            }
        }
    }
}
