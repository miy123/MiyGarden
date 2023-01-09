using MiyGarden.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiyGarden.Service.LeetCode
{
    /// <summary>
    /// Given an integer array nums, find the contiguous subarray (containing at least one number) which has the largest sum and return its sum.
    /// Follow up: If you have figured out the O(n) solution, try coding another solution using the divide and conquer approach, which is more subtle.
    /// https://leetcode.com/problems/maximum-subarray/
    /// </summary>
    public class _0053_Maximum_Subarray : ILeetCode
    {
        public int Number => 53;

        public string[] Main()
        {
            var result = new string[]
            {
                MaxSubArray(new int[] { -2, 1, -3, 4, -1, 2, 1, -5, 4 }).ToString(),
                MaxSubArray(new int[] { 1 }).ToString(),
                MaxSubArray(new int[] { 0 }).ToString(),
                MaxSubArray(new int[] { -1 }).ToString(),
                MaxSubArray(new int[] { -2147483647 }).ToString()
            };
            foreach (var x in result)
                Console.WriteLine(x);
            return result;
        }

        public int MaxSubArray(int[] nums)
        {
            int i;
            var max = nums[0];
            var current = max;
            // begin from index:1
            for (i = 1; i < nums.Length; i++)
            {
                // 價值小於0的值直接捨棄
                if (current < 0)
                    current = nums[i];
                else
                {
                    // 與下個數相加後，價值小於0的值也捨棄
                    current += nums[i];
                    if (current < 0)
                        current = nums[i];
                }
                max = Math.Max(max, current);
            }
            return max;
        }
    }
}
