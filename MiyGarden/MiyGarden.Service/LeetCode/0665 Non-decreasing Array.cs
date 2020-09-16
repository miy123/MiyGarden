using MiyGarden.Service.Interfaces;
using System;
using System.Threading;

namespace MiyGarden.Service.LeetCode
{
    public class _0665_Non_decreasing_Array : ILeetCode
    {
        public int Number => 665;

        public void Main()
        {
            Console.WriteLine(true + "=" + this.CheckPossibility(new int[] { 4, 2, 3 }));
            Console.WriteLine(false + "=" + this.CheckPossibility(new int[] { 4, 2, 1 }));
            Console.WriteLine(false + "=" + this.CheckPossibility(new int[] { 3, 4, 2, 3 }));
            Console.WriteLine(true + "=" + this.CheckPossibility(new int[] { -1, 4, 2, 3 }));
            Console.WriteLine(false + "=" + this.CheckPossibility(new int[] { 2, 3, 3, 2, 2 }));
            Console.WriteLine(true + "=" + this.CheckPossibility(new int[] { 5, 7, 1, 8 }));
            Console.WriteLine(true + "=" + this.CheckPossibility(new int[] { 1, 4, 1, 2 }));
        }

        /// <summary>
        /// [自解] O(N)若第二大的數比訪問的數小，移掉此訪問-1位址的數(大數)；反之則移掉此訪問位址的數(小數)
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public bool CheckPossibility(int[] nums)
        {
            int i;
            var result = true;
            var max = -10 ^ 5 - 1;
            var sec = max;
            for (i = 0; i < nums.Length; i++)
            {
                if (max > nums[i])
                {
                    if (result)
                    {
                        if (sec <= nums[i])
                            max = nums[i];
                        result = false;
                    }
                    else
                        return false;
                }
                else
                {
                    sec = max;
                    max = nums[i];
                }
            }

            return true;
        }
    }
}
