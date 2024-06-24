using MiyGarden.Service.Interfaces;
using System;

namespace MiyGarden.Service.LeetCode
{
    /// <summary>
    /// Given an array of integers nums sorted in non-decreasing order, find the starting and ending position of a given target value.
    /// If target is not found in the array, return [-1, -1].
    /// You must write an algorithm with O(log n) runtime complexity.
    /// Example 1:
    /// Input: nums = [5, 7, 7, 8, 8, 10], target = 8
    /// Output: [3,4]
    /// Example 2:
    /// Input: nums = [5, 7, 7, 8, 8, 10], target = 6
    /// Output: [-1,-1]
    /// Example 3:
    /// Input: nums = [], target = 0
    /// Output: [-1,-1]
    /// Constraints:
    /// 0 <= nums.length <= 105
    /// -109 <= nums[i] <= 109
    /// nums is a non-decreasing array.
    /// -109 <= target <= 109
    /// </summary>
    public class _0034_Find_First_and_Last_Position_of_Element_in_Sorted_Array : ILeetCode
    {
        public int Number => 34;

        public string[] Main()
        {
            Console.WriteLine("3,4=" + string.Join(',', SearchRange(new int[] { 5, 7, 7, 8, 8, 10 }, 8)));
            Console.WriteLine("-1,-1=" + string.Join(',', SearchRange(new int[] { 5, 7, 7, 8, 8, 10 }, 6)));
            Console.WriteLine("-1,-1=" + string.Join(',', SearchRange(new int[] { }, 0)));
            return new string[] { };
        }

        public int[] SearchRange(int[] nums, int target)
        {
            var result = new int[] { -1, -1 };
            for (var i = 0; i < nums.Length; i++)
            {
                if (nums[i] == target)
                {
                    if (result[0] == -1) result[0] = i;
                    else result[1] = i;
                }
                else if (nums[i] > target)
                {
                    break;
                }
            }
            if (result[1] == -1) result[1] = result[0];
            return result;
        }
    }
}
