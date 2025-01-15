using MiyGarden.Service.Interfaces;
using System;
using System.Collections.Generic;

namespace MiyGarden.Service.LeetCode
{
    /// <summary>
    /// Given a m x n grid filled with non-negative numbers, find a path from top left to bottom right, which minimizes the sum of all numbers along its path.
    /// Note: You can only move either down or right at any point in time.
    /// Example 1:
    /// Input: grid = [[1,3,1],[1,5,1],[4,2,1]]
    /// Output: 7
    /// Explanation: Because the path 1 → 3 → 1 → 1 → 1 minimizes the sum.
    /// Example 2:
    /// Input: grid = [[1,2,3],[4,5,6]]
    /// Output: 12
    /// Constraints:
    /// m == grid.length
    /// n == grid[i].length
    /// 1 <= m, n <= 200
    /// 0 <= grid[i][j] <= 200
    /// </summary>
    public class _0064_Minimum_Path_Sum : ILeetCode
    {
        public int Number => 64;

        public string[] Main()
        {
            return new string[] {
            //$"7={MinPathSum(new int[][] { new int[] { 1, 3, 1 }, new int[] { 1, 5, 1 }, new int[] { 4, 2, 1 } })}, {Count}",
            //$"12={MinPathSum(new int[][] { new int[] { 1, 2, 3 }, new int[] { 4, 5, 6 } })}, {Count}",
            MinPathSum(new int[][] { new int[] { 3, 8, 6, 0, 5, 9, 9, 6, 3, 4, 0, 5, 7, 3, 9, 3 },
                new int[] { 0,9,2,5,5,4,9,1,4,6,9,5,6,7,3,2 },
                new int[] { 8,2,2,3,3,3,1,6,9,1,1,6,6,2,1,9 },
                new int[] { 1,3,6,9,9,5,0,3,4,9,1,0,9,6,2,7},
                new int[] { 8,6,2,2,1,3,0,0,7,2,7,5,4,8,4,8},
                new int[] { 4,1,9,5,8,9,9,2,0,2,5,1,8,7,0,9},
                new int[] { 6,2,1,7,8,1,8,5,5,7,0,2,5,7,2,1},
                new int[] { 8,1,7,6,2,8,1,2,2,6,4,0,5,4,1,3},
                new int[] { 9,2,1,7,6,1,4,3,8,6,5,5,3,9,7,3},
                new int[] { 0,6,0,2,4,3,7,6,1,3,8,6,9,0,0,8},
                new int[] { 4,3,7,2,4,3,6,4,0,3,9,5,3,6,9,3},
                new int[] { 2,1,8,8,4,5,6,5,8,7,3,7,7,5,8,3},
                new int[] { 0,7,6,6,1,2,0,3,5,0,8,0,8,7,4,3},
                new int[] { 0,4,3,4,9,0,1,9,7,7,8,6,4,6,9,5},
                new int[] { 6,5,1,9,9,2,2,7,4,2,7,2,2,3,7,2},
                new int[] { 7,1,9,6,1,2,7,0,9,6,6,4,4,5,1,0},
                new int[] { 3,4,9,2,8,3,1,2,6,9,7,0,2,4,2,0},
                new int[] { 5,1,8,8,4,6,8,5,2,4,1,6,2,2,9,7}
            }).ToString(),
            Count.ToString(),
            MinPathSum(new int[][] { new int[] { 1, 5, 5 }, new int[] { 5, 2, 3 }, new int[] { 1, 8, 1 } }).ToString(),

            //MinPathSum2(new int[][] { new int[] { 3, 8, 6, 0, 5, 9, 9, 6, 3, 4, 0, 5, 7, 3, 9, 3 },
            //    new int[] { 0,9,2,5,5,4,9,1,4,6,9,5,6,7,3,2 },
            //    new int[] { 8,2,2,3,3,3,1,6,9,1,1,6,6,2,1,9 },
            //    new int[] { 1,3,6,9,9,5,0,3,4,9,1,0,9,6,2,7},
            //    new int[] { 8,6,2,2,1,3,0,0,7,2,7,5,4,8,4,8},
            //    new int[] { 4,1,9,5,8,9,9,2,0,2,5,1,8,7,0,9},
            //    new int[] { 6,2,1,7,8,1,8,5,5,7,0,2,5,7,2,1},
            //    new int[] { 8,1,7,6,2,8,1,2,2,6,4,0,5,4,1,3},
            //    new int[] { 9,2,1,7,6,1,4,3,8,6,5,5,3,9,7,3},
            //    new int[] { 0,6,0,2,4,3,7,6,1,3,8,6,9,0,0,8},
            //    new int[] { 4,3,7,2,4,3,6,4,0,3,9,5,3,6,9,3},
            //    new int[] { 2,1,8,8,4,5,6,5,8,7,3,7,7,5,8,3},
            //    new int[] { 0,7,6,6,1,2,0,3,5,0,8,0,8,7,4,3},
            //    new int[] { 0,4,3,4,9,0,1,9,7,7,8,6,4,6,9,5},
            //    new int[] { 6,5,1,9,9,2,2,7,4,2,7,2,2,3,7,2},
            //    new int[] { 7,1,9,6,1,2,7,0,9,6,6,4,4,5,1,0},
            //    new int[] { 3,4,9,2,8,3,1,2,6,9,7,0,2,4,2,0},
            //    new int[] { 5,1,8,8,4,6,8,5,2,4,1,6,2,2,9,7}
            //}).ToString(),
            //Count.ToString(),
            //$"7={MinPathSum2(new int[][] { new int[] { 1, 3, 1 }, new int[] { 1, 5, 1 }, new int[] { 4, 2, 1 } })}, {Count}",
            //$"12={MinPathSum2(new int[][] { new int[] { 1, 2, 3 }, new int[] { 4, 5, 6 } })}, {Count}",
            };
        }

        public int MinPathSum(int[][] grid)
        {
            for (var i = grid.Length - 1; i >= 0; i--)
            {
                for (var j = grid[0].Length - 1; j >= 0; j--)
                {
                    if (i == grid.Length - 1 && j == grid[0].Length - 1) { }
                    else if (i == grid.Length - 1) grid[i][j] = grid[i][j] + grid[i][j + 1];
                    else if (j == grid[0].Length - 1) grid[i][j] = grid[i][j] + grid[i + 1][j];
                    else grid[i][j] = grid[i][j] + Math.Min(grid[i + 1][j], grid[i][j + 1]);
                }
            }
            return grid[0][0];
        }

        private Dictionary<string, int> Cache;
        private long Count;

        public int MinPathSum1(int[][] grid)
        {
            Count = 0;
            Cache = new Dictionary<string, int>();
            var i = 0;
            var j = 0;
            return RightOrLeft(grid.Length - 1, grid[0].Length - 1, i, j, grid);
        }
        private int RightOrLeft(int m, int n, int i, int j, int[][] grid)
        {
            Count++;
            if (i == m && n == j) return grid[i][j];
            int value;
            var a = -1;
            if (m >= i + 1) if (!Cache.TryGetValue($"{i + 1},{j}", out a)) a = RightOrLeft(m, n, i + 1, j, grid);
            var b = -1;
            if (n >= j + 1) if (!Cache.TryGetValue($"{i},{j + 1}", out b)) b = RightOrLeft(m, n, i, j + 1, grid);
            if (a == -1) value = b + grid[i][j];
            else if (b == -1) value = a + grid[i][j];
            else value = Math.Min(a, b) + grid[i][j];
            Cache[$"{i},{j}"] = value;
            return value;
        }

        public int MinPathSum2(int[][] grid)
        {
            Count = 0;
            Cache = new Dictionary<string, int>();
            var i = 0;
            var j = 0;
            return RightOrLeft2(grid.Length - 1, grid[0].Length - 1, i, j, grid);
        }

        private int RightOrLeft2(int m, int n, int i, int j, int[][] grid)
        {
            Count++;
            if (i == m && n == j) return grid[i][j];

            int value;
            var a = -1;
            if (m >= i + 1)
            {
                a = RightOrLeft2(m, n, i + 1, j, grid);
            }
            var b = -1;
            if (n >= j + 1)
            {
                b = RightOrLeft2(m, n, i, j + 1, grid);
            }

            if (a == -1)
            {
                value = b + grid[i][j];
            }
            else if (b == -1)
            {
                value = a + grid[i][j];
            }
            else value = Math.Min(a, b) + grid[i][j];

            return value;
        }
    }
}
