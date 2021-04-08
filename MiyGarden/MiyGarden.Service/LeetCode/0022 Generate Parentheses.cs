using MiyGarden.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiyGarden.Service.LeetCode
{
    public class _0022_Generate_Parentheses : ILeetCode
    {
        public int Number => 22;

        public string[] Main()
        {
            var result = new string[]
            {
                "() = " + string.Join(',', GenerateParenthesis(1)),
                "((())),(()()),(())(),()(()),()()() = " + string.Join(',', GenerateParenthesis(3)),
                "(((()))),((()())),((())()),((()))(),(()(())),(()()()),(()())(),(())(()),(())()(),()((())),()(()()),()(())(),()()(()),()()()() = " + string.Join(',', GenerateParenthesis(4))
            };
            foreach (var x in result)
                Console.WriteLine(x);
            return result;
        }

        public IList<string> GenerateParenthesis(int n)
        {
            var dictionary = new IList<string>[n + 1];
            return GenerateParenthesis(n, dictionary);
        }

        private IList<string> GenerateParenthesis(int n, IList<string>[] dictionary)
        {
            if (n == 1)
            {
                dictionary[1] = new string[] { "()" };
                return dictionary[1];
            }

            var result = new List<string>();
            int k;
            for (k = 1; k < n; k++)
            {
                var diff = n - k;
                if (diff > 0)
                {
                    foreach (var item1 in GetDic(k, dictionary))
                    {
                        foreach (var item2 in GetDic(diff, dictionary))
                            result.Add(item1 + item2);
                    }
                }
            }

            result.AddRange((GetDic(n - 1, dictionary)).Select(x => $"({x})"));
            return result.Distinct().OrderBy(x => x).ToArray();
        }

        private IList<string> GetDic(int index, IList<string>[] dictionary)
        {
            if (dictionary[index] != null)
                return dictionary[index];
            else
            {
                dictionary[index] = GenerateParenthesis(index);
                return dictionary[index];
            }
        }
    }
}
