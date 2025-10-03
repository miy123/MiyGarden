using MiyGarden.Service.Interfaces;
using System;
using System.Collections.Generic;

namespace MiyGarden.Service.LeetCode
{
    /// <summary>
    /// Given an array nums of n integers where nums[i] is in the range [1, n], return an array of all the integers in the range [1, n] that do not appear in nums.
    ///Example 1:
    ///Input: nums = [4, 3, 2, 7, 8, 2, 3, 1]
    ///Output: [5, 6]
    ///Example 2:
    ///
    ///Input: nums = [1, 1]
    ///Output: [2]
    ///Constraints:
    ///
    ///n == nums.length
    ///1 <= n <= 105
    ///1 <= nums[i] <= n
    ///Follow up: Could you do it without extra space and in O(n) runtime? You may assume the returned list does not count as extra space.
    /// </summary>
    public class _0448_Find_All_Numbers_Disappeared_in_an_Array : ILeetCode
    {
        public int Number => 448;

        public string[] Main()
        {
            return [

                "Output: [5, 6]=" + string.Join(",", FindDisappearedNumbers(new[] { 4, 3, 2, 7, 8, 2, 3, 1 })),
                "Output: [2]=" + string.Join(",", FindDisappearedNumbers(new[] { 1, 1 })),

                "Output: [5, 6]=" + string.Join(",", FindDisappearedNumbers2(new[] { 4, 3, 2, 7, 8, 2, 3, 1 })),
                "Output: [2]=" + string.Join(",", FindDisappearedNumbers2(new[] { 1, 1 })),
                ];
        }

        public IList<int> FindDisappearedNumbers(int[] nums)
        {
            var arr = new int[nums.Length + 1];
            for (var i = 0; i < nums.Length; i++)
            {
                arr[nums[i]] = 1;
            }

            var result = new List<int>();
            for (var i = 1; i < arr.Length; i++)
            {
                if (arr[i] == 0) result.Add(i);
            }

            return result;
        }
        public IList<int> FindDisappearedNumbers2(int[] nums)
        {
            for (var i = 0; i < nums.Length; i++)
            {
                // var current = Math.Abs(nums[i]);
                // nums[current - 1] = Math.Abs(nums[current - 1]) * -1;
                nums[Math.Abs(nums[i]) - 1] = Math.Abs(nums[Math.Abs(nums[i]) - 1]) * -1;
            }

            var result = new List<int>();
            for (var i = 0; i < nums.Length; i++)
            {
                if (nums[i] > 0) result.Add(i + 1);
            }

            return result;
        }
    }
}
