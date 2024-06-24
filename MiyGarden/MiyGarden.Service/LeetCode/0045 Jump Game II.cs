using MiyGarden.Service.Interfaces;
using System;
using System.Collections.Generic;

namespace MiyGarden.Service.LeetCode
{
    /// <summary>
    /// You are given a 0-indexed array of integers nums of length n. You are initially positioned at nums[0].
    /// Each element nums[i] represents the maximum length of a forward jump from index i.In other words, if you are at nums[i], you can jump to any nums[i + j] where:
    /// 0 <= j <= nums[i] and i + j<n
    /// Return the minimum number of jumps to reach nums[n - 1]. The test cases are generated such that you can reach nums[n - 1].
    /// Example 1: 
    ///     Input: nums = [2, 3, 1, 1, 4]
    ///     Output: 2
    ///     Explanation: The minimum number of jumps to reach the last index is 2. Jump 1 step from index 0 to 1, then 3 steps to the last index.
    /// Example 2:
    ///     Input: nums = [2, 3, 0, 1, 4]
    ///     Output: 2
    /// Constraints:
    /// 1 <= nums.length <= 104
    /// 0 <= nums[i] <= 1000
    /// It's guaranteed that you can reach nums[n - 1].
    /// </summary>
    public class _0045_Jump_Game_II : ILeetCode
    {
        public int Number => 45;

        public string[] Main()
        {
            Console.WriteLine("2=" + Jump(new int[] { 2, 3, 1, 1, 4 }));
            Console.WriteLine("2=" + Jump(new int[] { 2, 3, 0, 1, 4 }));
            Console.WriteLine("0=" + Jump(new int[] { 1 }));
            Console.WriteLine("1=" + Jump(new int[] { 1, 2 }));
            Console.WriteLine("3=" + Jump(new int[] { 3, 4, 3, 2, 5, 4, 3 }));
            return new string[] { };
        }

        public int Jump(int[] nums)
        {
            if (nums.Length == 1) return 0;
            var road = new List<int>(nums.Length) { 0 };
            var checkPoint = nums[0];
            var mp = 0;
            var mi = 0;
            for (var i = 0; i < nums.Length; i++)
            {
                var point = nums[i] + i;
                if (point > mp)
                {
                    mi = i;
                    mp = point;
                }
                if (i == checkPoint || i == nums.Length - 1)
                {
                    checkPoint = nums[mi] + mi;
                    road.Add(mi);
                }
            }
            return road.Count - 1;
        }
    }
}
