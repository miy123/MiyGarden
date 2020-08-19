using MiyGarden.Service.Interfaces;
using System;

namespace MiyGarden.Service.LeetCode
{
    public class _10_Regular_Expression_Matching : ILeetCode
    {
        public int Number => 10;

        public void Main()
        {
            Console.WriteLine("False=" + this.IsMatch("aa", "a"));
            Console.WriteLine("True=" + this.IsMatch("aa", "a*"));
            Console.WriteLine("True=" + this.IsMatch("ab", ".b"));
            Console.WriteLine("True=" + this.IsMatch("abcdw", ".*"));
            Console.WriteLine("True=" + this.IsMatch("abcdw", "a.*"));
            Console.WriteLine("False=" + this.IsMatch("mississippi", "mis*is*p*."));
            Console.WriteLine("True=" + this.IsMatch("mississippi", "mis*is*ip*."));
            Console.WriteLine("True=" + this.IsMatch("", ""));
            Console.WriteLine("False=" + this.IsMatch("a", ""));
            Console.WriteLine("False=" + this.IsMatch("", "a"));
            Console.WriteLine("True=" + this.IsMatch("", "a*"));
            Console.WriteLine("True=" + this.IsMatch("aab", "c*a*b"));
            Console.WriteLine("True=" + this.IsMatch("aaa", "a*a"));
            Console.WriteLine("True=" + this.IsMatch("abcdcbwcb", ".*cb"));
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
