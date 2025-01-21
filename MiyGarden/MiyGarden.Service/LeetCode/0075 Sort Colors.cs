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
                "0,0,1,1,2,2,2,2=" + string.Join(',', this.SortColors(new int[] { 0, 2, 2, 2, 0, 2, 1, 1 })),
                "0,1,2=" + string.Join(',',this.SortColors(new int[] { 1,2,0 })),
                "0,1,2=" + string.Join(',',this.SortColors(new int[] { 1, 0, 2 })),
                "0,0,1,1,2,2=" + string.Join(',',this.SortColors(new int[] { 2, 0, 2, 1, 1, 0 })),
                "0,1,2=" + string.Join(',',this.SortColors(new int[] { 2, 0, 1 })),
                "0,1=" + string.Join(',',this.SortColors(new int[] { 0, 1 })),
                "0,1,2=" + string.Join(',',this.SortColors(new int[] { 0, 2, 1 })),
                "0,0,0,1,1,1=" + string.Join(',',this.SortColors(new int[] { 0,0,1,0,1,1 })),
            };
        }

        public int[] SortColors(int[] nums)
        {
            QuickSortInPlace(nums, 0, nums.Length - 1);
            return nums;
        }

        private void Sort(int[] nums, int first, int last)
        {
            if (first >= last || last - first < 1) return;
            if (last - first == 1)
            {
                if (nums[first] > nums[last]) (nums[last], nums[first]) = (nums[first], nums[last]);
                return;
            }
            var p = nums[first];
            var lj = last;
            var ll = -1;
            for (var i = first; i <= lj; i++)
            {
                if (nums[i] >= p)
                {
                    var ch = false;
                    for (var j = lj; j > i; j--)
                    {
                        lj = j - 1;
                        if (nums[j] < p)
                        {
                            (nums[j], nums[i]) = (nums[i], nums[j]);
                            if (lj == i) ch = true;
                            break;
                        }
                    }

                    if (i == lj)
                    {
                        if (i == first || ch) ll = lj;
                        else ll = lj - 1;
                    }
                }
            }

            Sort(nums, first, ll);
            Sort(nums, ll + 1, last);
        }

        private void QuickSortInPlace(int[] array, int low, int high)
        {
            if (low < high)
            {
                // Partition the array and get the pivot index
                // Choose the last element as the pivot
                int i = low - 1;
                for (int j = low; j < high; j++)
                {
                    if (array[j] <= array[high])
                    {
                        i++;
                        (array[i], array[j]) = (array[j], array[i]);
                    }
                }

                // Place the pivot in its correct position
                (array[i + 1], array[high]) = (array[high], array[i + 1]);
                int pivotIndex = i + 1;

                // Recursively sort elements before and after partition
                QuickSortInPlace(array, low, pivotIndex - 1);
                QuickSortInPlace(array, pivotIndex + 1, high);
            }
        }
    }
}
