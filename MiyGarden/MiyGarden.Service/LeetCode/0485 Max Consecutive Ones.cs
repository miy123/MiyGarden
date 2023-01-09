using MiyGarden.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiyGarden.Service.LeetCode
{
    public class _0485_Max_Consecutive_Ones : ILeetCode
    {
        public int Number => 485;

        public string[] Main()
        {
            var result = new string[]
            {
                FindMaxConsecutiveOnes(new int[] { 1, 1, 0, 1, 1, 1 }).ToString()
            };
            foreach (var x in result)
                Console.WriteLine(x);
            return result;
        }

        public int FindMaxConsecutiveOnes(int[] nums)
        {
            var max = 0;
            var temp = 0;
            int i;
            for (i = 0; i < nums.Length; i++)
            {
                if (nums[i] == 1)
                    temp++;
                else
                    temp = 0;
                if (temp > max) max = temp;
            }

            return max;
        }
    }
}
