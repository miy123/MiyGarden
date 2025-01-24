using MiyGarden.Service.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace MiyGarden.Service.LeetCode
{
    /// <summary>
    /// Given an integer array nums of unique elements, return all possible subsets(the power set).
    /// The solution set must not contain duplicate subsets.Return the solution in any order.
    /// Example 1:
    /// Input: nums = [1, 2, 3]
    /// Output: [[], [1], [2], [1,2], [3], [1,3], [2,3], [1,2,3]]
    /// Example 2:
    /// Input: nums = [0]
    ///     Output: [[],[0]]
    /// Constraints:
    /// 1 <= nums.length <= 10
    /// -10 <= nums[i] <= 10
    /// All the numbers of nums are unique.
    /// </summary>
    public class _0078_Subsets : ILeetCode
    {
        public int Number => 78;

        public string[] Main()
        {
            var nums = new int[] { 1, 2, 3 };
            return new string[] { JsonConvert.SerializeObject(Subsets(nums)) };
        }

        // 數列的總和可以歸納為：S=2n−1
        public IList<IList<int>> Subsets(int[] nums)
        {
            var subsets = new List<IList<int>>
            {
                new List<int>()
            };
            // 1 2 3
            // []
            // 1
            // 2 12
            // 3 13 23 123
            // 4 14 24 124 34 234 1234
            for (int i = 0; i < nums.Length; i++)
            {
                int count = subsets.Count;
                for (int j = 0; j < count; j++)
                {
                    var temp = subsets[j].ToList();
                    temp.Add(nums[i]);
                    subsets.Add(temp);
                }
            }
            return subsets;
        }
    }
}
