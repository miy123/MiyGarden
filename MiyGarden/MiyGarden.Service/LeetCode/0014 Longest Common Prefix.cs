using MiyGarden.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiyGarden.Service.LeetCode
{
    public class _14_Longest_Common_Prefix : ILeetCode
    {
        public int Number => 14;

        public string[] Main()
        {
            //Console.WriteLine("fl=" + this.LongestCommonPrefix2(new string[] { "flower", "flow", "flight" }));
            //Console.WriteLine("=" + this.LongestCommonPrefix2(new string[] { "dog", "racecar", "car" }));
            //Console.WriteLine("a=" + this.LongestCommonPrefix2(new string[] { "aa", "ab" }));

            var result = new string[]
            {
                "fl=" + this.LongestCommonPrefix(new string[] { "flower", "flow", "flight" }),
                "=" + this.LongestCommonPrefix(new string[] { "dog", "racecar", "car" }),
            };
            foreach (var x in result)
                Console.WriteLine(x);
            return result;
        }

        /// <summary>
        /// 遍歷所有字串，逐個字元比對
        /// </summary>
        /// <param name="strs"></param>
        /// <returns></returns>
        public string LongestCommonPrefix(string[] strs)
        {
            if (strs.Length == 0) return string.Empty;
            var min = int.MaxValue;
            var temp = default(char);
            int i;
            int j = 0;
            var result = new StringBuilder();
            for (i = 0; i < strs.Length; i++)
            {
                var str = strs[i];
                if (str == string.Empty) return string.Empty;
                if (temp == default(char)) temp = str[j];
                if (str.Length < min) min = str.Length;
                if (temp != str[j]) return string.Empty;
                else if (i == strs.Length - 1)
                {
                    result.Append(temp);
                    temp = default(char);
                    j++;
                }
            }

            for (; j < min; j++)
            {
                for (i = 0; i < strs.Length; i++)
                {
                    var str = strs[i];
                    if (temp == default(char))
                        temp = str[j];
                    if (temp != str[j])
                        return result.ToString();
                    else if (i == strs.Length - 1)
                    {
                        result.Append(temp);
                        temp = default(char);
                    }
                }
            }

            return result.ToString();
        }

        //public string LongestCommonPrefix2(string[] strs)
        //{
        //    if (strs.Length == 0) return string.Empty;
        //    var NO_OF_CHARS = 256;
        //    var table = new int[NO_OF_CHARS];

        //    var num = 0;
        //    var max = int.MaxValue;
        //    foreach (var str in strs)
        //    {
        //        if (str == string.Empty) return string.Empty;
        //        int i = 0;
        //        for (; i < str.Length; i++)
        //        {
        //            var ch = str[i];
        //            if (table[ch] == num)
        //                table[ch]++;
        //            else
        //            {
        //                if (max < i)
        //                    max = i;
        //                break;
        //            }
        //        }

        //        if (max == 0)
        //            break;

        //        num++;
        //    }

        //    var result = new StringBuilder();
        //    var j = 0;
        //    for (; j < strs[0].Length; j++)
        //    {
        //        var str = strs[0][j];
        //        if (table[str] == num)
        //            result.Append(str);
        //        else
        //            break;
        //    }

        //    return result.ToString();
        //}
    }
}
