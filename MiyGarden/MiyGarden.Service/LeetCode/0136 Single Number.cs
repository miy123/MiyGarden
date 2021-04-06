using MiyGarden.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiyGarden.Service.LeetCode
{
    public class _0136_Single_Number : ILeetCode
    {
        public int Number => 136;

        public void Main()
        {
            //Console.WriteLine("1=" + SingleNumber(new int[] { 2, 2, 1 }));
            Console.WriteLine("4=" + SingleNumber(new int[] { 9, 4, 1, 2, 1, 6, 2, 9, 6 }));
            //Console.WriteLine("1=" + SingleNumber(new int[] { 1 }));
        }

        public int SingleNumber(int[] nums)
        {
            var hash = new HashSet<int>(nums.Length);
            int i;
            for (i = 0; i < nums.Length; i++)
            {
                if (!hash.Add(nums[i]))
                    hash.Remove(nums[i]);
            }
            return hash.First();
        }
    }
}
