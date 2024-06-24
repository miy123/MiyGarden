using MiyGarden.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MiyGarden.Service.LeetCode
{
    /// <summary>
    /// Given an array of distinct integers candidates and a target integer target, return a list of all unique combinations of candidates where the chosen numbers sum to target. You may return the combinations in any order.
    ///     The same number may be chosen from candidates an unlimited number of times.Two combinations are unique if the
    ///     frequency
    ///  of at least one of the chosen numbers is different.
    /// The test cases are generated such that the number of unique combinations that sum up to target is less than 150 combinations for the given input.
    /// Example 1:
    /// Input: candidates = [2,3,6,7], target = 7
    /// Output: [[2,2,3],[7]]
    /// Explanation:
    /// 2 and 3 are candidates, and 2 + 2 + 3 = 7. Note that 2 can be used multiple times.
    /// 7 is a candidate, and 7 = 7.
    /// These are the only two combinations.
    /// Example 2:
    /// Input: candidates = [2, 3, 5], target = 8
    /// Output: [[2,2,2,2], [2,3,3], [3,5]]
    /// Example 3:
    /// Input: candidates = [2], target = 1
    /// Output: []
    ///     Constraints:
    /// 1 <= candidates.length <= 30
    /// 2 <= candidates[i] <= 40
    /// All elements of candidates are distinct.
    /// 1 <= target <= 40
    /// </summary>
    public class _0039_Combination_Sum : ILeetCode
    {
        public int Number => 39;

        public string[] Main()
        {
            Console.WriteLine("[[2,2,3],[7]]=" + string.Join('|', CombinationSum(new int[] { 2, 3, 6, 7 }, 7).Select(x => string.Join(',', x))));
            Console.WriteLine("[[2,2,2,2], [2,3,3], [3,5]]=" + string.Join('|', CombinationSum(new int[] { 2, 3, 5 }, 8).Select(x => string.Join(',', x))));
            Console.WriteLine("[]=" + string.Join('|', CombinationSum(new int[] { 2 }, 1).Select(x => string.Join(',', x))));
            return new string[] { };
        }

        public IList<IList<int>> CombinationSum(int[] candidates, int target)
        {
            var result = new List<IList<int>>();
            var currentCombination = new List<int>();
            FindCombinations(candidates, 0, candidates.Length, target, currentCombination, result);
            return result;
        }

        public void FindCombinations(int[] nums, int start, int end, int target, List<int> currentCombination, List<IList<int>> result)
        {
            var sum = currentCombination.Sum();
            if (sum == target)
            {
                if (!result.Select(x => string.Join(',', x)).Contains(string.Join(',', currentCombination)))
                    result.Add(new List<int>(currentCombination));
                return;
            }
            else if (sum > target)
            {
                return;
            }

            for (int i = start; i < end; i++)
            {
                currentCombination.Add(nums[i]);
                FindCombinations(nums, i, i + 1, target, currentCombination, result);
                FindCombinations(nums, i + 1, nums.Length, target, currentCombination, result);
                currentCombination.RemoveAt(currentCombination.Count - 1);
            }
        }
    }
}
