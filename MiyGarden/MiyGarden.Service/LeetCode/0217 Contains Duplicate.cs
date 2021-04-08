using MiyGarden.Service.Interfaces;
using System;
using System.Collections.Generic;

namespace MiyGarden.Service.LeetCode
{
    public class _217_Contains_Duplicate : ILeetCode
    {
        public int Number => 217;

        public string[] Main()
        {
            var result = new string[]
            {
                ContainsDuplicate(new int[] { 1, 2, 0, 1 }).ToString(),
                ContainsDuplicate(new int[] { 1, 2, 3 }).ToString()
            };
            foreach (var x in result)
                Console.WriteLine(x);
            return result;
        }

        public bool ContainsDuplicate(int[] nums)
        {
            var result = new HashSet<int>();
            foreach (var num in nums)
            {
                if (!result.Add(num))
                    return true;
            }

            return false;
        }
    }
}
