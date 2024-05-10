using MiyGarden.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MiyGarden.Service.LeetCode
{
    /// <summary>
    /// A permutation of an array of integers is an arrangement of its members into a sequence or linear order.
    ///   For example, for arr = [1,2,3], the following are all the permutations of arr: [1,2,3], [1,3,2], [2, 1, 3], [2, 3, 1], [3,1,2], [3,2,1].
    /// The next permutation of an array of integers is the next lexicographically greater permutation of its integer.More formally,
    /// if all the permutations of the array are sorted in one container according to their lexicographical order, 
    /// then the next permutation of that array is the permutation that follows it in the sorted container.
    /// If such arrangement is not possible, the array must be rearranged as the lowest possible order (i.e., sorted in ascending order).
    /// 
    /// For example, the next permutation of arr = [1, 2, 3] is [1, 3, 2].
    /// Similarly, the next permutation of arr = [2,3,1] is [3,1,2].
    /// While the next permutation of arr = [3, 2, 1] is [1, 2, 3] because [3,2,1] does not have a lexicographical larger rearrangement.
    ///     Given an array of integers nums, find the next permutation of nums.
    ///     The replacement must be in place and use only constant extra memory.
    ///     Example 1:
    /// 
    /// Input: nums = [1,2,3]
    ///     Output: [1,3,2]
    ///     Example 2:
    /// 
    /// Input: nums = [3,2,1]
    ///     Output: [1,2,3]
    ///     Example 3:
    /// 
    /// Input: nums = [1,1,5]
    ///     Output: [1,5,1]
    ///     Constraints:
    /// 
    /// 1 <= nums.length <= 100
    /// 0 <= nums[i] <= 100
    /// </summary>
    public class _0031_Next_Permutation : ILeetCode
    {
        public int Number => 31;
        public string[] Main()
        {
            NextPermutation(new int[] { 1, 2, 3 });
            NextPermutation(new int[] { 1, 2, 3, 4 });
            return new string[] { };
        }

        public void NextPermutation(int[] nums)
        {
            var freeList = nums.OrderBy(x => x).ToList();
            var count = 0;
            var currenct = new List<int>();
            NextPermutation(nums, 0, freeList, currenct, ref count);
        }

        private void NextPermutation(int[] nums, int i, List<int> orderedFreeList, List<int> currentList, ref int count)
        {
            for (var k = 0; k < orderedFreeList.Count; k++)
            {
                if (orderedFreeList[k] == nums[i] || count == 1)
                {
                    var nextOrderedFreeList = orderedFreeList.Except(new List<int>() { orderedFreeList[k] }).ToList();
                    currentList.Add(orderedFreeList[k]);
                    if (nextOrderedFreeList.Count == 0)
                    {
                        Console.WriteLine(string.Join(",", currentList));
                        if (count == 1)
                        {
                            Console.WriteLine("end.");
                            count++;
                        }
                        else
                        {
                            count++;
                            currentList.Remove(orderedFreeList[k]);
                        }
                    }

                    NextPermutation(nums, i + 1, nextOrderedFreeList, currentList, ref count);
                    if (count == 1) currentList.Remove(orderedFreeList[k]);
                }
                if (count == 2) continue;
            }
        }
    }
}
