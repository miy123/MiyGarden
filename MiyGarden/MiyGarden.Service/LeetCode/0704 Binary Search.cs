using MiyGarden.Service.Interfaces;

namespace MiyGarden.Service.LeetCode
{
    /// <summary>
    /// Given an array of integers nums which is sorted in ascending order, and an integer target, write a function to search target in nums. 
    /// If target exists, then return its index. Otherwise, return -1.
    ///    You must write an algorithm with O(log n) runtime complexity.
    ///    Example 1:
    ///Input: nums = [-1, 0, 3, 5, 9, 12], target = 9
    ///    Output: 4
    ///Explanation: 9 exists in nums and its index is 4
    ///Example 2:
    ///    Input: nums = [-1, 0, 3, 5, 9, 12], target = 2
    ///    Output: -1
    ///Explanation: 2 does not exist in nums so return -1
    ///    Constraints:
    ///1 <= nums.length <= 104
    ///-104 < nums[i], target< 104
    ///All the integers in nums are unique.
    ///    nums is sorted in ascending order.
    /// </summary>
    public class _704_Binary_Search : ILeetCode
    {
        public int Number => 543;

        public string[] Main()
        {
            var a = Search(new int[] { -1, 0, 3, 5, 9, 12 }, 9);

            return new string[] { };
        }

        public int Search(int[] nums, int target)
        {
            var half = nums.Length / 2;
            if (nums[half] > target)
            {
                for (var i = 0; i < half; i++)
                {
                    if (nums[i] == target) return i;
                    else if (nums[i] > target) return -1;
                }
            }
            else if (nums[half] == target)
            {
                return half;
            }
            else
            {
                for (var i = half + 1; i < nums.Length; i++)
                {
                    if (nums[i] == target) return i;
                    else if (nums[i] > target) return -1;
                }
            }
            return -1;
        }
    }
}
