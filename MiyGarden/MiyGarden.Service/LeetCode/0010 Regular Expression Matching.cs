using MiyGarden.Service.Interfaces;
using System;

namespace MiyGarden.Service.LeetCode
{
    public class _10_Regular_Expression_Matching : ILeetCode
    {
        public int Number => 10;

        public string[] Main()
        {

            var result = new string[]
            {
            "False=" + this.IsMatch("aa", "a"),
            "True=" + this.IsMatch("aa", "a*"),
            "True=" + this.IsMatch("ab", ".b"),
            "True=" + this.IsMatch("abcdw", ".*"),
            "True=" + this.IsMatch("abcdw", "a.*"),
            "False=" + this.IsMatch("mississippi", "mis*is*p*."),
            "True=" + this.IsMatch("mississippi", "mis*is*ip*."),
            "True=" + this.IsMatch("", ""),
            "False=" + this.IsMatch("a", ""),
            "False=" + this.IsMatch("", "a"),
            "True=" + this.IsMatch("", "a*"),
            "True=" + this.IsMatch("aab", "c*a*b"),
            "True=" + this.IsMatch("aaa", "a*a"),
            "True=" + this.IsMatch("abcdcbwcb", ".*cb")
            };
            foreach (var x in result)
                Console.WriteLine(x);
            return result;
        }

        /// <summary>
        /// 遞迴分段
        /// </summary>
        /// <param name="s"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool IsMatch(string s, string p)
        {
            if (string.IsNullOrEmpty(p))
                return string.IsNullOrEmpty(s);

            if (p.Length > 1 && p[1] == '*')
            {
                if (s.Length > 0 && (s[0] == p[0] || p[0] == '.'))
                    return this.IsMatch(s.Substring(1), p) || this.IsMatch(s, p.Substring(2));
                else
                    return this.IsMatch(s, p.Substring(2));
            }
            else
            {
                if (string.IsNullOrEmpty(s))
                    return string.IsNullOrEmpty(p);
                else if (p[0] == '.' || s[0] == p[0])
                    return this.IsMatch(s.Substring(1), p.Substring(1));
                else
                    return false;
            }
        }
    }
}
