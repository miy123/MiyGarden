using MiyGarden.Service.Interfaces;
using System;

namespace MiyGarden.Service.LeetCode
{
    /// <summary>
    /// Given two strings word1 and word2, return the minimum number of operations required to convert word1 to word2.
    /// You have the following three operations permitted on a word:
    /// Insert a character
    /// Delete a character
    /// Replace a character
    /// Example 1:
    /// Input: word1 = "horse", word2 = "ros"
    /// Output: 3
    /// Explanation: 
    /// horse -> rorse(replace 'h' with 'r')
    /// rorse -> rose(remove 'r')
    /// rose -> ros(remove 'e')
    /// Example 2:
    /// Input: word1 = "intention", word2 = "execution"
    /// Output: 5
    /// Explanation: 
    /// intention -> inention(remove 't')
    /// inention -> enention(replace 'i' with 'e')
    /// enention -> exention(replace 'n' with 'x')
    /// exention -> exection(replace 'n' with 'c')
    /// exection -> execution(insert 'u')
    /// Constraints:
    /// 0 <= word1.length, word2.length <= 500
    /// word1 and word2 consist of lowercase English letters.
    /// </summary>
    public class _0072_Edit_Distance : ILeetCode
    {
        public int Number => 72;

        public string[] Main()
        {
            throw new NotImplementedException();
        }

        public int MinDistance(string word1, string word2)
        {
            var min = Math.Max(word1.Length, word1.Length);
            var maxValue = 0;
            for (var i = 0; i < word1.Length; i++)
            {
                var final = false;
                var deep = false;

                var firstIndex = -1;
                var lastIndex = -1;
                var nowJ = -1;
                for (var j = 0; j < word2.Length; j++)
                {
                    if (word1[i] == word2[j])
                    {
                        if (firstIndex == -1) firstIndex = i;
                        lastIndex = i;
                        nowJ = j;
                        break;
                    }
                }

                if (firstIndex != -1)
                {
                    var x = firstIndex;
                    var y = nowJ;
                    var diff = 0;
                    var value = 1;
                    do
                    {
                        x++;
                        y++;
                        if (word1[x] == word2[y])
                        {
                            value++;
                        }
                        else
                        {
                            diff++;
                        }

                    } while (x == word1.Length - 1 || y == word2.Length - 1);
                    var step = nowJ - firstIndex;
                    maxValue = Math.Max(maxValue, value);
                }
            }
            return 0;
        }
    }
}
