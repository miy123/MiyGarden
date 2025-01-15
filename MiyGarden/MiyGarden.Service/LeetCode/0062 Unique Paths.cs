using MiyGarden.Service.Interfaces;
using System.Collections.Generic;

namespace MiyGarden.Service.LeetCode
{
    /// <summary>
    /// There is a robot on an m x n grid. The robot is initially located at the top-left corner (i.e., grid[0][0]). The robot tries to move to the bottom-right corner (i.e., grid[m - 1][n - 1]). The robot can only move either down or right at any point in time.
    /// Given the two integers m and n, return the number of possible unique paths that the robot can take to reach the bottom-right corner.
    /// The test cases are generated so that the answer will be less than or equal to 2 * 109.
    /// Example 1:
    /// Input: m = 3, n = 7
    /// Output: 28
    /// Example 2:
    /// Input: m = 3, n = 2
    /// Output: 3
    /// Explanation: From the top-left corner, there are a total of 3 ways to reach the bottom-right corner:
    /// 1. Right -> Down -> Down
    /// 2. Down -> Down -> Right
    /// 3. Down -> Right -> Down
    /// Constraints:
    /// 1 <= m, n <= 100
    /// </summary>
    public class _0062_Unique_Paths : ILeetCode
    {
        public int Number => 62;

        public string[] Main()
        {
            return new string[] { "3=" + UniquePaths(3, 2), "28=" + UniquePaths3(3, 7).ToString(),
                UniquePaths(5, 1).ToString(), UniquePaths(5, 2).ToString(), UniquePaths(5, 3).ToString(), UniquePaths(5, 4).ToString(),
                UniquePaths(5, 5).ToString(), UniquePaths(5, 6).ToString(),
                // 4 10 20 35 56
                UniquePaths(4, 1).ToString(), UniquePaths(4, 2).ToString(), UniquePaths(4, 3).ToString(), UniquePaths(4, 4).ToString(),
                UniquePaths(4, 5).ToString(), UniquePaths(4, 6).ToString(),
                // 3 6 10 15
                UniquePaths(3, 1).ToString(), UniquePaths(3, 2).ToString(), UniquePaths(3, 3).ToString(), UniquePaths(3, 4).ToString(),
                UniquePaths(3, 5).ToString(), UniquePaths(3, 6).ToString(),
                // 2 3 4 5
                // 1 3 6 10
                UniquePaths(2, 1).ToString(), UniquePaths(2, 2).ToString(), UniquePaths(2, 3).ToString(), UniquePaths(2, 4).ToString(),
                UniquePaths(2, 5).ToString(), UniquePaths(2, 6).ToString(),
                // 1 1 1 1
                // 1 2 3 4 5 6
                UniquePaths(1, 1).ToString(), UniquePaths(1, 2).ToString(), UniquePaths(1, 3).ToString(), UniquePaths(1, 4).ToString(),
                UniquePaths(1, 5).ToString(), UniquePaths(1, 6).ToString()
            };
        }

        private readonly Dictionary<string, int> Cache = new();

        public int UniquePaths(int m, int n)
        {
            if (m == 1 || n == 1) { 
                return 1;
            }
            int r1;
            if (Cache.ContainsKey($"{m},{n - 1}")) r1 = Cache[$"{m},{n - 1}"];
            else
            {
                r1 = UniquePaths(m, n - 1);
                Cache[$"{m},{n - 1}"] = r1;
            }
            int r2;
            if (Cache.ContainsKey($"{m - 1},{n}")) r2 = Cache[$"{m - 1},{n}"];
            else
            {
                r2 = UniquePaths(m - 1, n);
                Cache[$"{m - 1},{n}"] = r2;
            }

            return r1 + r2;
        }

        public int UniquePaths2(int m, int n)
        {
            var i = 0;
            var j = 0;
            return RightOrLeft(m - 1, n - 1, i, j);
        }

        private int RightOrLeft(int m, int n, int i, int j)
        {
            if (i == m || n == j) return 1;
            var count = 0;
            if (m >= i + 1)
            {
                count += RightOrLeft(m, n, i + 1, j);
            }

            if (n >= j + 1)
            {
                count += RightOrLeft(m, n, i, j + 1);
            }

            return count;
        }

        public int UniquePaths3(int m, int n)
        {
            long ans = 1;
            for (int i = 1; i <= m - 1; i++)
            {
                ans = ans * (n - 1 + i) / i;
            }
            return (int)ans;
        }
    }
}
