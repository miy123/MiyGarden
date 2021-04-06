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
            Console.WriteLine(ContainsDuplicate(new int[] { 1, 2, 0, 1 }));
            Console.WriteLine(ContainsDuplicate(new int[] { 1, 2, 3 }));
            var result = new string[]
            {
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
