using MiyGarden.Service.Interfaces;
using System;

namespace MiyGarden.Service.LeetCode
{
    /// <summary>
    /// There is an integer array nums sorted in ascending order (with distinct values).
    /// Prior to being passed to your function, nums is possibly rotated at an unknown pivot index k(1 <= k<nums.length) such that the resulting array is [nums[k], nums[k + 1], ..., nums[n - 1], nums[0], nums[1], ..., nums[k - 1]] (0-indexed).
    /// For example, [0, 1, 2, 4, 5, 6, 7] might be rotated at pivot index 3 and become[4, 5, 6, 7, 0, 1, 2].
    /// Given the array nums after the possible rotation and an integer target, return the index of target if it is in nums, or -1 if it is not in nums.
    /// You must write an algorithm with O(log n) runtime complexity.
    /// Example 1:
    /// Input: nums = [4, 5, 6, 7, 0, 1, 2], target = 0
    /// Output: 4
    /// Example 2:
    /// Input: nums = [4, 5, 6, 7, 0, 1, 2], target = 3
    /// Output: -1
    /// Example 3:
    /// Input: nums = [1], target = 0
    /// Output: -1
    /// Constraints:
    /// 1 <= nums.length <= 5000
    /// -104 <= nums[i] <= 104
    /// All values of nums are unique.
    /// nums is an ascending array that is possibly rotated.
    /// -104 <= target <= 104
    /// </summary>
    public class _0033_Search_in_Rotated_Sorted_Array : ILeetCode
    {
        public int Number => 33;

        public string[] Main()
        {
            Console.WriteLine(Search(new int[] { 4, 5, 6, 7, 0, 1, 2 }, 0));
            Console.WriteLine(Search(new int[] { 8, 1, 2, 3, 4, 5, 7 }, 8));
            Console.WriteLine(Search(new int[] { 4, 5, 6, 7, 8, 1, 2, 3 }, 8));
            Console.WriteLine(Search(new int[] { 4, 5, 6, 7, 0, 1, 2 }, 3));
            Console.WriteLine(Search(new int[] { 1 }, 0));
            Console.WriteLine(Search(new int[] { 1 }, 1));
            Console.WriteLine(Search(new int[] { 1 }, 2));
            Console.WriteLine(Search(new int[] { 3, 1 }, 3));
            Console.WriteLine(Search(new int[] { 1, 3 }, 2));
            Console.WriteLine(Search(new int[] { 1, 3 }, 1));
            Console.WriteLine(Search(new int[] { 4, 5, 6, 7, 0, 1, 2 }, 6));
            Console.WriteLine(Search(new int[] { 5, 1, 2, 3, 4 }, 1));
            Console.WriteLine(Search(new int[] { 2, 3, 4, 5, 6, 7, 8, 9, 1 }, 9));

            return new string[] { };
        }

        /// <summary>
        /// 主要還是縮小定位在前段或後段
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public int Search(int[] nums, int target)
        {
            int start = 0, end = nums.Length - 1, mid;
            while (start <= end && start < nums.Length)
            {
                mid = (start + end) / 2;
                if (nums[mid] < target)
                {
                    if (nums[end] < target)
                    {
                        if (nums[mid] > nums[end])
                            start = mid + 1;
                        else
                            end = mid - 1;
                    }
                    else
                        start = mid + 1;
                }
                else if (nums[mid] > target)
                {
                    if (nums[start] > target)
                    {
                        if (nums[start] > nums[mid])
                            end = mid - 1;
                        else
                            start = mid + 1;
                    }
                    else
                        end = mid - 1;
                }
                else return mid;
            }
            return -1;
        }
    }
}
