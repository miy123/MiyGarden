using MiyGarden.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiyGarden.Service.LeetCode
{
    public class _13_Roman_to_Integer : ILeetCode
    {
        public int Number => 13;

        private int count;

        public string[] Main()
        {
            count = 0;
            Console.WriteLine("3=" + this.RomanToInt("III"));
            count = 0;
            Console.WriteLine("4=" + this.RomanToInt("IV"));
            count = 0;
            Console.WriteLine("9=" + this.RomanToInt("IX"));
            count = 0;
            Console.WriteLine("58=" + this.RomanToInt("LVIII"));
            count = 0;
            Console.WriteLine("1994=" + this.RomanToInt("MCMXCIV"));
            Console.WriteLine("58=" + this.RomanToInt2("LVIII"));
            Console.WriteLine("1994=" + this.RomanToInt2("MCMXCIV"));

            var result = new string[]
            {
            };
            foreach (var x in result)
                Console.WriteLine(x);
            return result;
        }

        public int RomanToInt(string s)
        {
            if (s == string.Empty) return count;
            var first = s[0].ToString();
            if (first == "I")
            {
                if (s.Length == 1) return this.Process1(1, 1, s);
                var sec = s[1].ToString();
                if (sec == "V")
                    return this.Process1(4, 2, s);
                else if (sec == "X")
                    return this.Process1(9, 2, s);
                else
                    return this.Process1(1, 1, s);
            }
            else if (first == "V")
                return this.Process1(5, 1, s);
            else if (first == "X")
            {
                if (s.Length == 1) return this.Process1(10, 1, s);
                var sec = s[1].ToString();
                if (sec == "L")
                    return this.Process1(40, 2, s);
                else if (sec == "C")
                    return this.Process1(90, 2, s);
                else
                    return this.Process1(10, 1, s);
            }
            else if (first == "L")
                return this.Process1(50, 1, s);
            else if (first == "C")
            {
                if (s.Length == 1) return this.Process1(100, 1, s);
                var sec = s[1].ToString();
                if (sec == "D")
                    return this.Process1(400, 2, s);
                else if (sec == "M")
                    return this.Process1(900, 2, s);
                else
                    return this.Process1(100, 1, s);
            }
            else if (first == "M")
                return this.Process1(1000, 1, s);
            else if (first == "D")
                return this.Process1(500, 1, s);

            throw new ArgumentException("");
        }

        private int Process1(int countss, int sub, string s)
        {
            count += countss;
            return this.RomanToInt(s.Substring(sub));
        }

        public int RomanToInt2(string s)
        {
            var dict = new Dictionary<char, int>()
            {
                {'I', 1 },
                {'V', 5 },
                {'X', 10 },
                {'L', 50 },
                {'C', 100 },
                {'D', 500 },
                {'M', 1000 },
            };

            var result = 0;
            int i;
            for (i = 0; i < s.Length; i++)
            {
                if (i < s.Length - 1 && dict[s[i + 1]] > dict[s[i]])
                {
                    var val = dict[s[i + 1]] - dict[s[i]];
                    result += val;
                    i++;
                }
                else
                    result += dict[s[i]];

            }
            return result;
        }
    }
}
