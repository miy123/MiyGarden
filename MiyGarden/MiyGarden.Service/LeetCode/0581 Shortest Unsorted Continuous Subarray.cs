using MiyGarden.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiyGarden.Service.LeetCode
{
    public class _0581_Shortest_Unsorted_Continuous_Subarray : ILeetCode
    {
        public int Number => 581;

        public string[] Main()
        {
            var result = new string[]
            {
                "5=" + this.FindUnsortedSubarray(new int[] { 2, 6, 4, 8, 10, 9, 15 }).ToString(),
                "3=" + this.FindUnsortedSubarray(new int[] { 2, 3, 3, 2, 4 }).ToString(),
                "3=" + this.FindUnsortedSubarray(new int[] { 1, 2, 4, 5, 3 }).ToString(),
                "4=" + this.FindUnsortedSubarray(new int[] { 1, 3, 2, 5, 4 }).ToString(),
                "4=" + this.FindUnsortedSubarray(new int[] { 1, 3, 5, 4, 2 }).ToString()
            };
            foreach (var x in result)
                Console.WriteLine(x);
            return result;
        }

        public int FindUnsortedSubarray(int[] nums)
        {
            var max = -int.MaxValue;
            var min = int.MaxValue;
            int start = -1;
            int end = -1;
            var isNormal = true;
            var same = 0;
            int i;
            for (i = 0; i < nums.Length; i++)
            {
                var pre = i == 0 ? max : nums[i - 1];
                if (i != 0 && nums[i] == nums[i - 1])
                {
                    same++;
                    continue;
                }

                if (nums[i] < pre)
                {
                    if (start == -1)
                    {
                        int j;
                        for (j = i - 1; j >= 0; j--)
                        {
                            if (nums[i] < nums[j])
                                start = j;
                            else
                                break;
                        }
                    }
                    else
                    {
                        isNormal = true;
                        end = -1;
                    }
                }

                if (start != -1 && nums[i] > pre && nums[i] >= max && isNormal)
                {
                    end = i - 1;
                    isNormal = false;
                }

                if (max < nums[i]) max = nums[i];
                if (min > nums[i]) min = nums[i];
            }

            if (start == -1) return 0;
            if (end == -1) return nums.Length - start;

            return end - start + 1;
        }
    }
}
