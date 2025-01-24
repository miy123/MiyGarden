using MiyGarden.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MiyGarden.Service.LeetCode
{
    /// <summary>
    /// Given two strings text1 and text2, return the length of their longest common subsequence. If there is no common subsequence, return 0.
    ///    A subsequence of a string is a new string generated from the original string with some characters(can be none) deleted without changing the relative order of the remaining characters.
    ///    For example, "ace" is a subsequence of "abcde".
    /// A common subsequence of two strings is a subsequence that is common to both strings.
    ///    Example 1:
    ///    Input: text1 = "abcde", text2 = "ace"
    ///    Output: 3  
    /// Explanation: The longest common subsequence is "ace" and its length is 3.
    /// Example 2:
    ///     Input: text1 = "abc", text2 = "abc"
    ///     Output: 3
    /// Explanation: The longest common subsequence is "abc" and its length is 3.
    /// Example 3:
    ///     Input: text1 = "abc", text2 = "def"
    ///     Output: 0
    /// Explanation: There is no such common subsequence, so the result is 0.
    ///    Constraints:
    /// 1 <= text1.length, text2.length <= 1000
    /// text1 and text2 consist of only lowercase English characters.
    /// </summary>
    public class _1143_Longest_Common_Subsequence : ILeetCode
    {
        public int Number => 1143;

        public string[] Main()
        {
            return new string[]
            {
                "5=" + LongestCommonSubsequence("abcba", "abcbcba").ToString(),
                //"1=" + LongestCommonSubsequence("bsbininm", "jmjkbkjkv").ToString(),
                "3=" + LongestCommonSubsequence("abcde", "ace").ToString(),
                //"3=" + LongestCommonSubsequence("abc", "abc").ToString(),
                "0=" + LongestCommonSubsequence("abc", "def").ToString()
            };
        }

        public int LongestCommonSubsequence(string text1, string text2)
        {
            var map = new List<(char, List<int>)>();
            for (var i = 0; i < text1.Length; i++)
            {
                map.Add((text1[i], new List<int>()));
            }

            for (var j = 0; j < text2.Length; j++)
            {
                var tmp = map.Where(x => x.Item1 == text2[j]).Select(x => x.Item2);
                foreach (var item in tmp)
                {
                    item.Add(j + 1);
                }
            }

            var max = 0;
            var re = new List<List<int>>() { new List<int>() { 0 } };
            for (var j = 0; j < map.Count; j++)
            {
                re.Insert(j + 1, new List<int>());
                if (!re[j].Any()) re[j] = re[j - 1];
                foreach (var i in map[j].Item2)
                {
                    for (var k = 0; k < re[j].Count; k++)
                    {
                        if (i != 0)
                        {
                            if (i > max)
                            {
                                re[j + 1].Add((re[j][k] + 1));
                            }
                            else
                            {
                                re[j + 1].Add(1);
                            }
                            max = i;
                        }
                    }
                }
            }

            return re.SelectMany(x => x).Max();
        }

        public int LongestCommonSubsequence2(string text1, string text2)
        {
            if (text1 == text2) return text1.Length;
            // Make text1 the shorter string
            if (text1.Length > text2.Length)
            {
                string temp = text1;
                text1 = text2;
                text2 = temp;
            }

            int m = text1.Length;
            int n = text2.Length;
            // Check if strings share any characters at all
            if (!HasCommonChars(text1, text2)) return 0;
            // Find common prefix and suffix to reduce problem size
            int prefixLen = GetCommonPrefixLength(text1, text2);
            int suffixLen = GetCommonSuffixLength(text1, prefixLen, text2, prefixLen);
            // If the strings are all common prefix and suffix
            if (prefixLen + suffixLen >= Math.Min(m, n))
            {
                return Math.Min(m, n);
            }
            // Trim strings to remove common prefix and suffix
            text1 = text1.Substring(prefixLen, m - prefixLen - suffixLen);
            text2 = text2.Substring(prefixLen, n - prefixLen - suffixLen);

            m = text1.Length;
            n = text2.Length;
            //DP
            int[] dp = new int[n + 1];

            for (int i = 1; i <= m; i++)
            {
                int prev = 0;
                for (int j = 1; j <= n; j++)
                {
                    int temp = dp[j];
                    if (text1[i - 1] == text2[j - 1])
                    {
                        dp[j] = prev + 1;
                    }
                    else
                    {
                        dp[j] = Math.Max(dp[j], dp[j - 1]);
                    }
                    prev = temp;
                }
            }

            return dp[n] + prefixLen + suffixLen;
        }

        private bool HasCommonChars(string s1, string s2)
        {
            bool[] chars = new bool[128];

            for (int i = 0; i < s1.Length; i++)
            {
                chars[s1[i]] = true;
            }

            for (int i = 0; i < s2.Length; i++)
            {
                if (chars[s2[i]]) return true;
            }

            return false;
        }

        private int GetCommonPrefixLength(string s1, string s2)
        {
            int len = 0;
            int minLen = Math.Min(s1.Length, s2.Length);

            while (len < minLen && s1[len] == s2[len])
            {
                len++;
            }

            return len;
        }

        private int GetCommonSuffixLength(string s1, int s1Start, string s2, int s2Start)
        {
            int len = 0;
            int i = s1.Length - 1;
            int j = s2.Length - 1;

            while (i >= s1Start && j >= s2Start && s1[i] == s2[j])
            {
                len++;
                i--;
                j--;
            }

            return len;
        }
    }
}
