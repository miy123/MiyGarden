using Microsoft.EntityFrameworkCore.Internal;
using MiyGarden.Service.Interfaces;
using System;
using System.Collections.Generic;

namespace MiyGarden.Service.LeetCode
{
    public class _0020_Valid_Parentheses : ILeetCode
    {
        public int Number => 20;

        public string[] Main()
        {
            var result = new string[]
            {
                "true = " + IsValid2("()"),
                "true = " + IsValid2("()[]{}"),
            "false = " + IsValid2("(]"),
            "false = " + IsValid2("([)]"),
            "true = " + IsValid2("{[]}"),
            "false = " + IsValid2("["),
            "false = " + IsValid2("(("),
            "true = " + IsValid2("(([]){})")
            };
            foreach (var x in result)
                Console.WriteLine(x);
            return result;
        }

        private int count;

        /// <summary>
        /// [自解] 遞迴 O(N)
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public bool IsValid(string s)
        {
            if (s.Length % 2 == 1) return false;
            count = 0;
            var map = new Dictionary<char, char>()
            {
                {
                    '(' ,
                    ')'
                },
                {
                    '{' ,
                    '}'
                },
                {
                    '[',
                    ']'
                }
            };
            return Valid(s, 0, map) == '/';
        }

        public char Valid(string str, int index, Dictionary<char, char> symbols)
        {
            count = index;
            if (str.Length <= index) return '/';
            var currentChar = str[index];
            if (symbols.ContainsKey(currentChar))
            {
                if (symbols[currentChar] == Valid(str, index + 1, symbols))
                    return Valid(str, count + 1, symbols);
            }
            else return currentChar;
            return default(char);
        }

        /// <summary>
        /// [網路解] 使用stack O(N)
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public bool IsValid2(string s)
        {
            var st = new Stack<char>();
            int i;
            for (i = 0; i < s.Length; i++)
            {
                var cuc = s[i];
                if (cuc == '(' || cuc == '{' || cuc == '[')
                    st.Push(cuc);
                else
                {
                    if (st.Count == 0) return false;
                    var ce = st.Pop();
                    if (!((cuc == ')' && ce == '(') || (cuc == '}' && ce == '{') || (cuc == ']' && ce == '[')))
                        return false;
                }
            }

            return st.Count == 0;
        }
    }
}
