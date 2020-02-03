using MiyGarden.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MiyGarden.Service.LeetCode
{
    public class _10_Regular_Expression_Matching : ILeetCode
    {
        public int Number => 10;

        public void Main()
        {
            //Console.WriteLine("False=" + this.IsMatch("aa", "a"));
            //Console.WriteLine("True=" + this.IsMatch("aa", "a*"));
            //Console.WriteLine("True=" + this.IsMatch("ab", ".b"));
            //Console.WriteLine("True=" + this.IsMatch("abcdw", ".*"));
            Console.WriteLine("False=" + this.IsMatch("ab", ".*c"));
            //Console.WriteLine("True=" + this.IsMatch("abcdw", "a.*"));
            //Console.WriteLine("True=" + this.IsMatch("abcdwab", ".*ab"));
            //Console.WriteLine("False=" + this.IsMatch("abcdwab", ".*abc"));
            //Console.WriteLine("False=" + this.IsMatch("mississippi", "mis*is*p*."));
            //Console.WriteLine("True=" + this.IsMatch("mississippi", "mis*is*ip*."));
            //Console.WriteLine("True=" + this.IsMatch("", ""));
            //Console.WriteLine("False=" + this.IsMatch("a", ""));
            //Console.WriteLine("False=" + this.IsMatch("", "a"));
            //Console.WriteLine("True=" + this.IsMatch("", "a*"));
            //Console.WriteLine("True=" + this.IsMatch("aab", "c*a*b"));
        }

        public bool IsMatch(string s, string p)
        {
            var PList = new List<Pattern>();
            for (var i = 0; i < p.Length; i++)
            {
                PList.Add(this.Check(i, p, out int index));
                i = index;
            }

            if (string.IsNullOrEmpty(s))
            {
                if ((PList.Count == 1 && PList[0].PatternChar == '*') || string.IsNullOrEmpty(p))
                    return true;
                else
                    return false;
            }

            return PList.Any(x => x.PatternChar == '*' && x.PrecedingElement == '.') ?
                this.CheckSp(s, 0, PList) : this.CheckNormal(s, PList);
        }

        private bool CheckNormal(string s, List<Pattern> pList)
        {
            var startIndex = 0;
            foreach (var item in pList)
            {
                if (!item.Check(s, startIndex, out int newIndex))
                    return false;
                startIndex = newIndex;
            }

            return s.Length == startIndex;
        }

        private bool CheckSp(string s, int index, List<Pattern> pList)
        {
            var result = false;
            for (var j = index; j < s.Length; j++)
            {
                var sindex = j;
                foreach (var item in pList)
                {
                    if (!item.Check(s, sindex, out int newIndex))
                    {
                        result = false;
                        break;
                    }
                    else
                        result = true;

                    sindex = newIndex + 1;
                }
            }

            return result;
        }

        private Pattern Check(int index, string p, out int newIndex)
        {
            newIndex = index;
            if (index + 1 >= p.Length)
                return new Pattern()
                {
                    PatternChar = p[index],
                    CurrentElement = p[index]
                };

            if (p[index + 1] == '*')
            {
                newIndex++;
                return new Pattern()
                {
                    PatternChar = '*',
                    PrecedingElement = p[index]
                };
            }
            else
                return new Pattern()
                {
                    PatternChar = p[index],
                    CurrentElement = p[index]
                };
        }

        public class Pattern
        {
            public char PrecedingElement { get; set; }

            public char CurrentElement { get; set; }

            public char PatternChar { get; set; }

            public bool Check(string s, int startIndex, out int newIndex)
            {
                bool result;
                newIndex = startIndex;
                switch (this.PatternChar)
                {
                    case '*':
                        if (this.PrecedingElement == '.')
                            newIndex++;
                        if (s[startIndex] == this.PrecedingElement)
                            newIndex = GetIndex(startIndex) + 1;

                        return true;
                    case '.':
                        newIndex++;
                        return true;
                    default:
                        result = this.CurrentElement == s[startIndex];
                        if (result)
                            newIndex++;
                        return result;

                        int GetIndex(int findStartIndex)
                        {
                            if (findStartIndex + 1 >= s.Length || s[findStartIndex + 1] != s[findStartIndex])
                                return findStartIndex;
                            else
                                return GetIndex(findStartIndex + 1);
                        }
                }
            }
        }
    }
}
