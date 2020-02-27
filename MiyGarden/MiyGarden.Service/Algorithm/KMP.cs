using System;
using System.Collections.Generic;
using System.Text;

namespace MiyGarden.Service.Algorithm
{
    public class Kmp
    {
        public void Main()
        {
            //Console.WriteLine(string.Join(',', this.GetFailureFunction("ABCDABD")));
            //Console.WriteLine(string.Join(',', this.GetFailureFunction("ABCABA")));
            //Console.WriteLine(string.Join(',', this.GetFailureFunction("ababbaababbabaa")));
            //Console.WriteLine(this.Match("AHADSDABDABCABCDABD", "ABCDABD"));
            this.KMPSearch("ABCEABACABABDABD", "ABABD");
            this.KMPSearch("aabaaabaaac", "aabaaac");
            Console.WriteLine(string.Join(',',GetFailureFunction("ABCABCBABDA")));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public int Match(string target, string pattern)
        {
            /// 失配時，pattern向右移動的距離 = 已匹配字元數 - 失配字元的上一位字元所對應的failurefunction值
            if (target.Length < pattern.Length)
                return -1;
            var failurefunction = this.GetFailureFunction(pattern);
            var startIndex = 0;
            var start = 0;
            do
            {
                for (var i = start; i < pattern.Length; i++)
                {
                    //if (startIndex + i >= target.Length)
                    //    break;
                    if (target[startIndex + i] != pattern[i])
                    {
                        var next = i - (i == 0 ? 0 : failurefunction[i - 1]);
                        startIndex += next <= 0 ? 1 : next;
                        start = next <= 0 ? 1 : next;
                        break;
                    }
                    if (i == pattern.Length - 1)
                        return startIndex;
                }
            } while (startIndex < target.Length - pattern.Length);

            return -1;
        }

        void KMPSearch(string target, string pattern)
        {
            int[] failurefunction = GetFailureFunction(pattern);
            int patternIndex = 0;
            int targetIndex = 0;
            while (targetIndex < target.Length)
            {
                if (pattern[patternIndex] == target[targetIndex])
                {
                    patternIndex++;
                    targetIndex++;
                }
                else
                {
                    if (patternIndex != 0)
                        patternIndex = failurefunction[patternIndex - 1];
                    else
                        targetIndex++;
                }

                if (patternIndex == pattern.Length)
                {
                    Console.Write($"Found pattern at index {targetIndex - patternIndex}");
                    patternIndex = failurefunction[patternIndex - 1];
                }
            }
        }

        private int[] GetFailureFunction(string pattern)
        {
            int len = 0;
            var resultIndex = 1;
            var result = new int[pattern.Length];

            while (resultIndex < pattern.Length)
            {
                if (pattern[resultIndex] == pattern[len])
                {
                    len++;
                    result[resultIndex] = len;
                    resultIndex++;
                }
                else
                {
                    if (len != 0)
                        len = result[len - 1];
                    else
                        resultIndex++;
                }
            }

            return result;

            //var result = new int[pattern.Length];
            //var resultIndex = 1;

            //do
            //{
            //    var flag = false;
            //    for (var i = 0; i < pattern.Length; i++)
            //    {
            //        if (resultIndex < pattern.Length && pattern[i] == pattern[resultIndex] && result[resultIndex] == 0)
            //        {
            //            result[resultIndex] = i + 1;
            //            resultIndex++;
            //            flag = true;
            //        }
            //        else
            //            break;
            //    }

            //    if (!flag) resultIndex++;
            //} while (resultIndex < pattern.Length);

            //return result;
        }
    }
}
