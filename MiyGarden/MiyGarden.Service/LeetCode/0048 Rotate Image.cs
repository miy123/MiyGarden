using MiyGarden.Service.Interfaces;
using System.Collections.Generic;

namespace MiyGarden.Service.LeetCode
{
    /// <summary>
    /// You are given an n x n 2D matrix representing an image, rotate the image by 90 degrees (clockwise).
    /// You have to rotate the image in-place, which means you have to modify the input 2D matrix directly.DO NOT allocate another 2D matrix and do the rotation.
    /// Example 1:
    /// Input: matrix = [[1,2,3],[4,5,6],[7,8,9]]
    /// Output: [[7,4,1],[8,5,2],[9,6,3]]
    /// Example 2:
    /// Input: matrix = [[5,1,9,11],[2,4,8,10],[13,3,6,7],[15,14,12,16]]
    /// Output: [[15,13,2,5],[14,3,4,1],[12,6,8,9],[16,7,10,11]]
    /// Constraints:
    /// n == matrix.length == matrix[i].length
    /// 1 <= n <= 20
    /// -1000 <= matrix[i][j] <= 1000
    /// </summary>
    /// 

    public class _0048_Rotate_Image : ILeetCode
    {
        public int Number => 48;
        public string[] Main()
        {
            Rotate(new int[][] { new int[] { 1, 2, 3 }, new int[] { 4, 5, 6 }, new int[] { 7, 8, 9 } });
            return new string[] { };
        }
        public void Rotate(int[][] matrix)
        {
            var list = new List<string>();
            for (var i = 0; i < matrix.Length; i++)
            {
                list.Add("");
            }
            for (var i = 0; i < matrix.Length; i++)
            {
                for (var j = 0; j < matrix[i].Length; j++)
                {
                    list[j] += matrix[matrix.Length - 1 - i][j] + ",";
                }
            }
            for (var i = 0; i < matrix.Length; i++)
            {
                var l = list[i].Split(",");
                for (var j = 0; j < matrix[i].Length; j++)
                {
                    matrix[i][j] = int.Parse(l[j]);
                }
            }
        }

        /// <summary>
        /// 先I, J互換，再同ROW倒序交換
        /// </summary>
        /// <param name="matrix"></param>
        public void Rotate2(int[][] matrix)
        {
            int n = matrix.Length;

            // Step 1: Transpose the matrix
            for (int i = 0; i < n; i++)
            {
                for (int j = i; j < n; j++)
                {
                    // Swap elements across the diagonal
                    int temp = matrix[i][j];
                    matrix[i][j] = matrix[j][i];
                    matrix[j][i] = temp;
                }
            }

            // Step 2: Reverse each row
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n / 2; j++)
                {
                    // Swap elements in the row
                    int temp = matrix[i][j];
                    matrix[i][j] = matrix[i][n - j - 1];
                    matrix[i][n - j - 1] = temp;
                }
            }
        }
    }
}
