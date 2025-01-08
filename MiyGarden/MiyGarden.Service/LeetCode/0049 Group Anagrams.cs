using MiyGarden.Service.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace MiyGarden.Service.LeetCode
{
    /// <summary>
    /// Given an array of strings strs, group the anagrams together.You can return the answer in any order.
    /// 
    /// Example 1:
    /// Input: strs = ["eat", "tea", "tan", "ate", "nat", "bat"]
    /// Output: [["bat"], ["nat","tan"], ["ate","eat","tea"]]
    /// Explanation:
    /// There is no string in strs that can be rearranged to form "bat".
    /// The strings "nat" and "tan" are anagrams as they can be rearranged to form each other.
    /// The strings "ate", "eat", and "tea" are anagrams as they can be rearranged to form each other.
    /// 
    /// Example 2:
    /// Input: strs = [""]
    /// Output: [[""]]
    /// 
    /// Example 3:
    /// Input: strs = ["a"]
    /// Output: [["a"]]
    /// Constraints:
    /// 1 <= strs.length <= 104
    /// 0 <= strs[i].length <= 100
    /// strs[i] consists of lowercase English letters.
    /// </summary>
    public class _0049_Group_Anagrams : ILeetCode
    {
        public int Number => 49;

        public string[] Main()
        {
            var re = GroupAnagrams(new string[] { "eat", "tea", "tan", "ate", "nat", "bat" });
            re = GroupAnagrams(new string[] { "" });
            re = GroupAnagrams(new string[] { "a" });
            return new string[] { };
        }

        public IList<IList<string>> GroupAnagrams(string[] strs)
        {
            var result = new Dictionary<string, IList<string>>();
            for (var i = 0; i < strs.Length; i++)
            {
                var key = new string(strs[i].OrderBy(y => y).ToArray());
                if (!result.ContainsKey(key))
                {
                    result.Add(key, new List<string>() { strs[i] });
                }
                else
                {
                    result[key].Add(strs[i]);
                }
            }

            return result.Select(x => x.Value).ToList();
        }
    }
}
