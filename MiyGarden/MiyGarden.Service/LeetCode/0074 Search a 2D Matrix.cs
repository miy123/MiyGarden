using MiyGarden.Service.Interfaces;

namespace MiyGarden.Service.LeetCode
{
    /// <summary>
    /// You are given an m x n integer matrix matrix with the following two properties:
    /// Each row is sorted in non-decreasing order.
    /// The first integer of each row is greater than the last integer of the previous row.
    /// Given an integer target, return true if target is in matrix or false otherwise.
    /// You must write a solution in O(log(m* n)) time complexity.
    /// Example 1:
    /// Input: matrix = [[1, 3, 5, 7], [10,11,16,20], [23,30,34,60]], target = 3
    /// Output: true
    /// Example 2:
    /// Input: matrix = [[1,3,5,7],[10,11,16,20],[23,30,34,60]], target = 13
    /// Output: false
    /// Constraints:
    /// m == matrix.length
    /// n == matrix[i].length
    /// 1 <= m, n <= 100
    /// -104 <= matrix[i][j], target <= 104
    /// </summary>
    public class _0074_Search_a_2D_Matrix : ILeetCode
    {
        public int Number => 74;

        public string[] Main()
        {
            return new string[]
            {
                "true=" + this.SearchMatrix(new int[][]
                {
                    new int[] { 1, 3, 5, 7 },
                    new int[] { 10, 11, 16, 20 },
                    new int[] { 23, 30, 34, 60 }
                }, 3),
                "false=" + this.SearchMatrix(new int[][]
                {
                    new int[] { 1, 3, 5, 7 },
                    new int[] { 10, 11, 16, 20 },
                    new int[] { 23, 30, 34, 60 }
                }, 13)
            };
        }

        public bool SearchMatrix(int[][] matrix, int target)
        {
            var m = matrix.Length;
            var n = matrix[0].Length;
            var left = 0;
            var right = m * n - 1;
            int mid, midValue;
            while (left <= right)
            {
                mid = left + (right - left) / 2;
                midValue = matrix[mid / n][mid % n];
                if (midValue == target) return true;
                else if (midValue < target) left = mid + 1;
                else right = mid - 1;
            }
            return false;
        }
    }
}
