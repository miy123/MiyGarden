using MiyGarden.Service.Interfaces;
using System.Collections.Generic;

namespace MiyGarden.Service.LeetCode
{
    /// <summary>
    /// Given an array nums of size n, return the majority element.
    ///    The majority element is the element that appears more than ⌊n / 2⌋ times.You may assume that the majority element always exists in the array.
    ///    Example 1:
    ///Input: nums = [3, 2, 3]
    ///    Output: 3
    ///Example 2:
    ///    Input: nums = [2, 2, 1, 1, 1, 2, 2]
    ///    Output: 2
    ///    Constraints:
    ///    n == nums.length
    ///    1 <= n <= 5 * 104
    ///-109 <= nums[i] <= 109
    /// </summary>
    public class _0169_Majority_Element : ILeetCode
    {
        public int Number => 169;

        public string[] Main()
        {
            var a = MajorityElement(new int[] { 3, 2, 3 });
            a = MajorityElement(new int[] { 1 });
            return new string[] { };
        }

        public int MajorityElement(int[] nums)
        {
            if(nums.Length == 1) return nums[0];
            var dic = new Dictionary<int, int>(nums.Length);
            var countArray = new int[nums.Length];
            var basis = nums.Length / 2.0;
            var index = 0;
            foreach (var num in nums)
            {
                if (dic.TryGetValue(num, out var cIndex))
                {
                    countArray[cIndex]++;
                    if (countArray[cIndex] > basis)
                    {
                        return num;
                    }
                }
                else
                {
                    dic.Add(num, index);
                    countArray[index]++;
                    index++;
                }
            }
            return -1;
        }
    }
}
