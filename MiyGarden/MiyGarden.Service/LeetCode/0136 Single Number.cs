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

        public string[] Main()
        {
            var result = new string[]
            {
                "1=" + SingleNumber(new int[] { 2, 2, 1 }),
                "4=" + SingleNumber(new int[] { 9, 4, 1, 2, 1, 6, 2, 9, 6 }),
                "1=" + SingleNumber(new int[] { 1 })
            };
            foreach (var x in result)
                Console.WriteLine(x);
            return result;
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
