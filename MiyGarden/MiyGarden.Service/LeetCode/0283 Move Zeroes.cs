using MiyGarden.Service.Interfaces;

namespace MiyGarden.Service.LeetCode
{
    /// <summary>
    /// Given an integer array nums, move all 0's to the end of it while maintaining the relative order of the non-zero elements.
    /// Note that you must do this in-place without making a copy of the array.
    /// Example 1:
    /// Input: nums = [0,1,0,3,12]
    /// Output: [1,3,12,0,0]
    /// Example 2:
    /// Input: nums = [0]
    /// Output: [0]
    /// </summary>
    public class _0283_Move_Zeroes : ILeetCode
    {
        public int Number => 283;

        public string[] Main()
        {
            MoveZeroes(new int[] { 0, 1, 0, 3, 12 });
            MoveZeroes(new int[] { 0 });
            return new string[] { };
        }

        public void MoveZeroes(int[] nums)
        {
            var count = 0;
            for (var i = 0; i < nums.Length; i++)
            {
                if (nums[i] == 0)
                {
                    count++;
                }
                else
                {
                    nums[i - count] = nums[i];
                }
            }
            for (var i = nums.Length - count; i < nums.Length; i++)
            {
                nums[i] = 0;
            }
        }
    }
}
