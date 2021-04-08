using MiyGarden.Service.Interfaces;
using System;
using System.Collections.Generic;

namespace MiyGarden.Service.LeetCode
{
    public class _0017_Letter_Combinations_of_a_Phone_Number : ILeetCode
    {
        public int Number => 17;

        public string[] Main()
        {
            var result = new string[]
            {
                string.Join(',', LetterCombinations("23")),
                string.Join(',', LetterCombinations("")),
                string.Join(',', LetterCombinations("2"))
            };
            foreach (var x in result)
                Console.WriteLine(x);
            return result;
        }

        private int _count = 1;
        private readonly Dictionary<char, string[]> _mapping = new Dictionary<char, string[]>()
            {
                { '2' , new string[]{ "a" , "b" , "c"} },
                { '3' , new string[]{ "d", "e" , "f"} },
                { '4' , new string[]{ "g" , "h", "i" }},
                { '5' , new string[]{ "j" , "k", "l" }},
                { '6' , new string[]{ "m" , "n", "o" }},
                { '7' , new string[]{ "p" , "q", "r", "s" }},
                { '8' , new string[]{ "t" , "u", "v" }},
                { '9' , new string[]{ "w" , "x", "y" , "z" }},
            };

        public IList<string> LetterCombinations(string digits)
        {
            if (string.IsNullOrEmpty(digits)) return new string[] { };
            return Process(GetLink(digits, 0, ref _count), string.Empty);
        }

        private string[] Process(Link link, string result)
        {
            if (link.Next == null)
            {
                var re = new string[link.Current.Length];
                int i;
                for (i = 0; i < link.Current.Length; i++)
                {
                    re[i] = result + link.Current[i];
                }
                return re;
            }

            var res = new List<string>(_count);
            int j;
            for (j = 0; j < link.Current.Length; j++)
            {
                res.AddRange(Process(link.Next, result + link.Current[j]));
            }
            return res.ToArray();
        }

        private Link GetLink(string digits, int index, ref int count)
        {
            count *= _mapping[digits[index]].Length;
            if (index == digits.Length - 1)
            {
                return new Link()
                {
                    Current = _mapping[digits[index]],
                    Next = null
                };
            }

            return new Link()
            {
                Current = _mapping[digits[index]],
                Next = GetLink(digits, index + 1, ref count)
            };
        }

        private class Link
        {
            public string[] Current { set; get; }

            public Link Next { get; set; }
        }
    }
}
