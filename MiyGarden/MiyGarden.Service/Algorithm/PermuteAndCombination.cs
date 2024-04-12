using System.Collections.Generic;

namespace MiyGarden.Service.Algorithm
{
    public class PermuteAndCombination
    {
        /// <summary>
        /// 組合
        /// </summary>
        /// <param name="list"></param>
        /// <param name="k"></param>
        /// <param name="result"></param>
        public static void Permute(List<int> list, int k, List<int> currentResult, List<List<int>> result)
        {
            currentResult ??= new List<int>();
            if (k == 0)
            {
                result.Add(new List<int>(currentResult));
                return;
            }

            for (int i = 0; i < list.Count; i++)
            {
                currentResult.Add(list[i]);
                var remainingList = new List<int>(list);
                remainingList.RemoveAt(i);
                Permute(remainingList, k - 1, currentResult, result);
                currentResult.RemoveAt(currentResult.Count - 1);
            }
        }

        public static void FindCombinations(int[] nums, int k, int start, List<int> currentCombination, List<List<int>> result)
        {
            if (currentCombination.Count == k)
            {
                result.Add(new List<int>(currentCombination));
                return;
            }

            for (int i = start; i < nums.Length; i++)
            {
                currentCombination.Add(nums[i]);
                FindCombinations(nums, k, i + 1, currentCombination, result);
                currentCombination.RemoveAt(currentCombination.Count - 1);
            }
        }
    }
}
