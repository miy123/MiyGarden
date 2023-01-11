using MiyGarden.Service.Interfaces;
using System;

namespace MiyGarden.Service.LeetCode
{
    public class _0035_Search_Insert_Position : ILeetCode
    {
        public int Number => 35;

        public string[] Main()
        {
            var result = new string[]
            {
                SearchInsert(new int[]{ 1,3,5,6},5).ToString(), //2
                SearchInsert(new int[]{ 1,3,5,6},2).ToString(), //1
                SearchInsert(new int[]{ 1,3,5,6},7).ToString() , //4
               SearchInsert(new int[]{ 1,3,5,6},0).ToString()  //
            };
            foreach (var x in result)
                Console.WriteLine(x);
            return result;
        }

        public int SearchInsert(int[] nums, int target)
        {
            for (var i = 0; i < nums.Length; i++)
            {
                if (target < nums[i])
                {
                    return i;
                }
                else if (target > nums[i])
                {
                    if (i == nums.Length - 1) return i + 1;
                }
                else // target == nums[i]
                    return i;
            }
            throw new ArgumentException("");
        }
    }
}
