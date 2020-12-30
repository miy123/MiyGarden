using MiyGarden.Service.Interfaces;
using System;

namespace MiyGarden.Service.LeetCode
{
    public class _01TwoSum : ILeetCode
    {
        public int Number => 1;

        public void Main()
        {
            Console.WriteLine("0,1=" + string.Join(',', this.TwoSum(new int[] { 2, 7, 11, 15 }, 9)));
            Console.WriteLine("0,1=" + string.Join(',', this.TwoSum1(new int[] { 2, 7, 11, 15 }, 9)));
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
    }
}
