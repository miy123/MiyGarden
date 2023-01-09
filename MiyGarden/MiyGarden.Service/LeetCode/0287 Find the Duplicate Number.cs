using MiyGarden.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiyGarden.Service.LeetCode
{
    public class _287_Find_the_Duplicate_Number : ILeetCode
    {
        public int Number => 287;

        public string[] Main()
        {
            var result = new string[]
            {
                this.FindDuplicate(new int[] { 1, 2, 3, 4, 2, 2 }).ToString(),
                this.FindDuplicate2(new int[] { 1, 2, 3, 4, 2, 2 }).ToString(),
                this.FindDuplicate2(new int[] { 13, 46, 8, 11, 20, 17, 40, 13, 13, 13, 14, 1, 13, 36, 48, 41, 13, 13, 13, 13, 45, 13, 28, 42, 13, 10, 15, 22, 13, 13, 13, 13, 23, 9, 6, 13, 47, 49, 16, 13, 13, 39, 35, 13, 32, 29, 13, 25, 30, 13 }).ToString()
            };
            foreach (var x in result)
                Console.WriteLine(x);
            return result;
        }

        public int FindDuplicate(int[] nums)
        {
            var hash = new HashSet<int>();
            foreach (var num in nums)
            {
                if (!hash.Add(num))
                    return num;
            }

            throw new Exception("");
        }

        public int FindDuplicate2(int[] nums)
        {
            var source = (ulong)Math.Pow(2, nums.Length) - 1;
            for (var i = 0; i < nums.Length; i++)
            {
                var temp = (ulong)Math.Pow(2, nums[i]);
                if ((source & temp) == temp)
                    source -= temp;
                else
                    return nums[i];
            }

            throw new Exception("");
        }
    }
}
