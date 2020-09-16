using MiyGarden.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiyGarden.Service.LeetCode
{
    public class _0581_Shortest_Unsorted_Continuous_Subarray : ILeetCode
    {
        public int Number => 581;

        public void Main()
        {
            Console.WriteLine("5=" + this.FindUnsortedSubarray(new int[] { 2, 6, 4, 8, 10, 9, 15 }));
        }

        public int FindUnsortedSubarray(int[] nums)
        {
            return 0;
        }
    }
}
