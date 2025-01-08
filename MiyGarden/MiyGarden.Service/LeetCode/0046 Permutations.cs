using MiyGarden.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MiyGarden.Service.LeetCode
{
    /// <summary>
    /// Given an array nums of distinct integers, return all the possible permutations.You can return the answer in any order.
    /// Example 1:
    /// Input: nums = [1, 2, 3]
    ///     Output: [[1,2,3], [1,3,2], [2,1,3], [2,3,1], [3,1,2], [3,2,1]]
    /// Example 2:
    /// Input: nums = [0,1]
    ///     Output: [[0,1],[1,0]]
    /// Example 3:
    /// Input: nums = [1]
    ///     Output: [[1]]
    /// Constraints:
    /// 1 <= nums.length <= 6
    /// -10 <= nums[i] <= 10
    /// All the integers of nums are unique.
    /// </summary>
    public class _0046_Permutations : ILeetCode
    {
        public int Number => 46;

        public string[] Main()
        {
            return Array.Empty<string>();
        }

        public IList<IList<int>> Permute(int[] nums)
        {
            var res = new List<IList<int>>();
            Permute(nums.ToList(), nums.Length, null, res);
            return res;
        }

        public void Permute(List<int> nums, int k, List<int> currentResult, List<IList<int>> result)
        {
            currentResult ??= new List<int>(nums.Count);
            if (k == 0)
            {
                result.Add(new List<int>(currentResult));
                return;
            }

            for (var i = 0; i < nums.Count; i++)
            {
                currentResult.Add(nums[i]);
                var remainingList = new List<int>(nums);
                remainingList.RemoveAt(i);
                Permute(remainingList, k - 1, currentResult, result);
                currentResult.RemoveAt(currentResult.Count - 1);
            }
        }
    }
}
