using MiyGarden.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiyGarden.Service.LeetCode
{
    public class _09_Palindrome_Number : ILeetCode
    {
        public int Number => 9;

        public string[] Main()
        {
            var result = new string[]
            {
                this.IsPalindrome(-123).ToString(),
                this.IsPalindrome(10).ToString(),
                this.IsPalindrome(121).ToString(),
                this.IsPalindrome(1).ToString(),
                this.IsPalindrome(100).ToString()
            };
            foreach (var x in result)
                Console.WriteLine(x);
            return result;
        }

        /// <summary>
        /// [自解] 倆倆比對
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public bool IsPalindrome(int x)
        {
            if (x < 0)
                return false;
            var source = x.ToString();
            int startIndex;
            int startIndexEnd;
            if (source.Length % 2 == 0)
            {
                startIndex = source.Length / 2 - 1;
                startIndexEnd = startIndex + 1;
                if (source[startIndex] != source[startIndexEnd])
                    return false;
            }
            else
            {
                startIndex = source.Length / 2;
                startIndexEnd = startIndex;
            }

            bool result = true;
            for (var iv = 1; iv < source.Length - startIndexEnd; iv++)
            {
                if (source[startIndex - iv] != source[iv + startIndexEnd])
                {
                    result = false;
                    break;
                }
            }

            return result;
        }
    }
}
