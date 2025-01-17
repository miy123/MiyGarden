using MiyGarden.Service.Interfaces;

namespace MiyGarden.Service.LeetCode
{
    /// <summary>
    /// Given an array nums with n objects colored red, white, or blue, sort them in-place so that objects of the same color are adjacent, with the colors in the order red, white, and blue.
    /// We will use the integers 0, 1, and 2 to represent the color red, white, and blue, respectively.
    /// You must solve this problem without using the library's sort function.
    /// Example 1:
    /// Input: nums = [2,0,2,1,1,0]
    ///     Output: [0,0,1,1,2,2]
    ///     Example 2:
    /// Input: nums = [2,0,1]
    ///     Output: [0,1,2]
    /// Constraints:
    /// n == nums.length
    /// 1 <= n <= 300
    /// nums[i] is either 0, 1, or 2.
    /// Follow up: Could you come up with a one-pass algorithm using only constant extra space?
    /// </summary>
    public class _0075_Sort_Colors : ILeetCode
    {
        public int Number => 75;

        public string[] Main()
        {
            return new string[]
            {
                //"0,0,1,1,2,2=" + string.Join(',',this.SortColors(new int[] { 2, 0, 2, 1, 1, 0 })),
                //"0,1,2=" + string.Join(',',this.SortColors(new int[] { 2, 0, 1 })),
                //"0,1=" + string.Join(',',this.SortColors(new int[] { 0, 1 })),
                //"0,1,2=" + string.Join(',',this.SortColors(new int[] { 0, 2, 1 })),
                "0,0,0,1,1,1=" + string.Join(',',this.SortColors(new int[] { 0,0,1,0,1,1 })),
            };
        }

        public int[] SortColors(int[] nums)
        {
            Sort(nums, 0, nums.Length - 1);
            return nums;
        }

        private void Sort(int[] nums, int first, int last)
        {
            if (first >= last) return;
            var lastJ = last;
            var p = nums[first];
            for (var i = first + 1; i <= lastJ; i++)
            {
                if (nums[i] > p)
                {
                    for (var j = lastJ; j > i; j--)
                    {
                        if (nums[j] < p)
                        {
                            (nums[j], nums[i]) = (nums[i], nums[j]);
                            lastJ = j - 1;
                            break;
                        }
                    }
                }
                else if (nums[i] == p)
                {
                    for (var j = lastJ; j > i; j--)
                    {
                        if (nums[j] < p)
                        {
                            (nums[j], nums[i]) = (nums[i], nums[j]);
                            lastJ = j - 1;
                            break;
                        }
                    }
                }
            }

            if (nums[first] >= nums[lastJ])
            {
                (nums[first], nums[lastJ]) = (nums[lastJ], nums[first]);
                Sort(nums, first, lastJ - 1);
                Sort(nums, lastJ + 1, last);
            }
            else
            {
                Sort(nums, first, lastJ - 1);
                Sort(nums, lastJ, last);
            }
        }
    }
}
