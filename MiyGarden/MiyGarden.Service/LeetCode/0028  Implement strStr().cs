using MiyGarden.Service.Interfaces;
using System;

namespace MiyGarden.Service.LeetCode
{
    public class _28__Implement_strStr__ : ILeetCode
    {
        public int Number => 28;

        public string[] Main()
        {
            var result = new string[]
            {
                "2=" + this.StrStr("hello", "ll"),
                "-1=" + this.StrStr("aaaaa", "bba"),
                "-1=" + this.StrStr("a", ""),
                "4=" + this.StrStr("aabaaabaaac", "aabaaac"),
                string.Join(",", this.GetFailureFunction("aabaaabaaac"))
            };
            foreach (var x in result)
                Console.WriteLine(x);
            return result;
        }

        /// <summary>
        /// KMP algorithms
        /// </summary>
        /// <param name="haystack">target text</param>
        /// <param name="needle">pattern</param>
        /// <returns></returns>
        public int StrStr(string haystack, string needle)
        {
            if (string.IsNullOrEmpty(haystack))
            {
                if (string.IsNullOrEmpty(needle))
                    return 0;
                else
                    return -1;
            }

            if (string.IsNullOrEmpty(needle))
                return 0;

            int[] failurefunction = GetFailureFunction(needle);
            int patternIndex = 0;
            int targetIndex = 0;
            while (targetIndex < haystack.Length)
            {
                if (needle[patternIndex] == haystack[targetIndex])
                {
                    patternIndex++;
                    targetIndex++;
                }

                if (patternIndex == needle.Length)
                {
                    return targetIndex - patternIndex;
                }
                else if (targetIndex < haystack.Length && needle[patternIndex] != haystack[targetIndex])
                {
                    if (patternIndex != 0)
                        patternIndex = failurefunction[patternIndex - 1];
                    else
                        targetIndex++;
                }
            }

            return -1;
        }

        private int[] GetFailureFunction(string pat)
        {
            int len = 0;
            int i = 1;
            int[] lps = new int[pat.Length];
            int M = pat.Length;

            while (i < M)
            {
                if (pat[i] == pat[len])
                {
                    len++;
                    lps[i] = len;
                    i++;
                }
                else
                {
                    if (len != 0)
                    {
                        len = lps[len - 1];
                    }
                    else
                    {
                        len = 0;
                        lps[i] = len;
                        i++;
                    }
                }
            }

            return lps;
        }
    }
}
