using MiyGarden.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiyGarden.Service.LeetCode
{
    public class _0485_Max_Consecutive_Ones : ILeetCode
    {
        public int Number => 485;

        public void Main()
        {
            Console.WriteLine(FindMaxConsecutiveOnes(new int[] { 1, 1, 0, 1, 1, 1 }));
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
