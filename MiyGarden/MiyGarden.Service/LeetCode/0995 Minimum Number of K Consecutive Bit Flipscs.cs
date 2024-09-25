using MiyGarden.Service.Interfaces;
using System;
using System.Diagnostics;

namespace MiyGarden.Service.LeetCode
{
    /// <summary>
    /// You are given a binary array nums and an integer k.
    ///      A k-bit flip is choosing a subarray of length k from nums and simultaneously changing every 0 in the subarray to 1, and every 1 in the subarray to 0.
    ///  Return the minimum number of k-bit flips required so that there is no 0 in the array.If it is not possible, return -1.
    ///  A subarray is a contiguous part of an array.
    ///  Example 1:
    ///  
    ///  Input: nums = [0, 1, 0], k = 1
    ///  Output: 2
    ///  Explanation: Flip nums[0], then flip nums[2].
    ///  Example 2:
    ///  
    ///  Input: nums = [1, 1, 0], k = 2
    ///  Output: -1
    ///  Explanation: No matter how we flip subarrays of size 2, we cannot make the array become [1,1,1].
    ///  Example 3:
    ///  
    ///  Input: nums = [0, 0, 0, 1, 0, 1, 1, 0], k = 3
    ///  Output: 3
    ///  Explanation: 
    ///  Flip nums[0], nums[1], nums[2]: nums becomes [1,1,1,1,0,1,1,0]
    ///  Flip nums[4], nums[5], nums[6]: nums becomes [1,1,1,1,1,0,0,0]
    ///  Flip nums[5], nums[6], nums[7]: nums becomes [1,1,1,1,1,1,1,1]
    ///  
    ///  Constraints:
    ///  1 <= nums.length <= 105
    ///  1 <= k <= nums.length
    /// </summary>
    public class _0995 : ILeetCode
    {
        public int Number => 995;

        public string[] Main()
        {
            Console.WriteLine("1=" + MinKBitFlips(new int[] { 0 }, 1));
            Console.WriteLine("0=" + MinKBitFlips(new int[] { 1 }, 1));
            Console.WriteLine("2=" + MinKBitFlips(new int[] { 0, 1, 0 }, 1));
            Console.WriteLine("-1=" + MinKBitFlips(new int[] { 1, 1, 0 }, 2));
            Console.WriteLine("3=" + MinKBitFlips2(new int[] { 0, 0, 0, 1, 0, 1, 1, 0 }, 3));
            return new string[] { };
        }

        /// <summary>
        /// 自解，每次翻每個index都記錄翻的次數
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public int MinKBitFlips(int[] nums, int k)
        {
            var count = 0;
            var checks = new int[nums.Length];
            var len = nums.Length - k + 1;
            for (var i = 0; i < len; i++)
            {
                if ((nums[i] == 0 && checks[i] % 2 == 0) || (nums[i] == 1 && checks[i] % 2 != 0))
                {
                    for (var j = i; j < i + k; j++)
                    {
                        checks[j]++;
                    }
                    count++;
                }
            }

            len = nums.Length - k - 1;
            for (var i = nums.Length - 1; i > len; i--)
            {
                if ((nums[i] == 0 && checks[i] % 2 == 0) || (nums[i] == 1 && checks[i] % 2 != 0))
                {
                    count = -1;
                    break;
                }
            }
            return count;
        }

        /// <summary>
        /// 他解，每次翻只記錄總數，用計算的方式確認每個index翻過幾次
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public int MinKBitFlips2(int[] nums, int k)
        {
            int flipCount = 0; // Tracks the current number of flips
            int result = 0;    // Tracks the total number of flips performed

            for (int i = 0; i < nums.Length; i++)
            {
                // If the current index is outside the range of the last flip window, adjust flipCount
                if (i >= k && nums[i - k] == -1)
                {
                    flipCount--;
                }
                // If the current bit needs to be flipped to become 1
                if (flipCount % 2 == nums[i])
                {
                    // If flipping is not possible because the remaining elements are less than k
                    if (i + k > nums.Length)
                    {
                        return -1;
                    }
                    // Mark the current position as flipped and update counters
                    nums[i] = -1;
                    // Increase the flip count
                    flipCount++;
                    // Increase the result
                    result++;
                }
            }

            return result;
        }
    }
}
