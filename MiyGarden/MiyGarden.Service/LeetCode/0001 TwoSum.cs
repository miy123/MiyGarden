using MiyGarden.Service.Interfaces;
using System;
using System.Collections.Generic;

namespace MiyGarden.Service.LeetCode
{
    public class _01TwoSum : ILeetCode
    {
        public int Number => 1;

        public string[] Main()
        {
            var result = new string[]
            {
                "0,1=" + string.Join(',', this.TwoSum(new int[] { 2, 7, 11, 15 }, 9)),
                "0,1=" + string.Join(',', this.TwoSum1(new int[] { 2, 7, 11, 15 }, 9)),
                "0,1=" + string.Join(',', this.TwoSum2(new int[] { 2, 7, 11, 15 }, 9))
            };
            foreach (var x in result)
                Console.WriteLine(x);
            return result;
        }

        public int[] TwoSum(int[] nums, int target)
        {
            var tempIndexs = new int[nums.Length];
            var index = 0;
            var tempValue = new int[nums.Length];
            for (var i = 0; i < nums.Length; i++)
            {
                if (i != 0)
                {
                    for (var j = 0; j < index; j++)
                    {
                        if (tempValue[j] == nums[i])
                        {
                            return new int[] { tempIndexs[j], i };
                        }
                    }
                }
                tempIndexs[index] = i;
                tempValue[index] = target - nums[i];
                index++;
            }
            throw new Exception("");
        }

        public int[] TwoSum1(int[] nums, int target)
        {
            int i;
            for (i = 0; i < nums.Length; i++)
            {
                int j;
                for (j = i + 1; j < nums.Length; j++)
                {
                    if (nums[i] + nums[j] == target)
                        return new int[] { i, j };
                }
            }
            throw new Exception("");
        }

        public int[] TwoSum2(int[] nums, int target)
        {
            var diff = new Dictionary<int, int>(nums.Length);
            for (var i = 0; i < nums.Length; i++)
            {
                if (diff.ContainsKey(nums[i])) return new int[] { i, diff[nums[i]] };
                else diff.TryAdd(target - nums[i], i);
            }

            throw new Exception("");
        }
    }
}
