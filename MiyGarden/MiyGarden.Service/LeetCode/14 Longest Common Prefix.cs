using MiyGarden.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiyGarden.Service.LeetCode
{
    public class _14_Longest_Common_Prefix : ILeetCode
    {
        public int Number => 14;

        public void Main()
        {

        }

        public string LongestCommonPrefix(string[] strs)
        {
            var NO_OF_CHARS = 256;
            var table = new int[NO_OF_CHARS];
            var match = this.GetData(strs[0], 0);

            foreach (var str in strs)
            {
                foreach (var cha in str)
                {
                    table[cha]++;
                }
            }

            return "";
        }

        Data GetData(string str, int i)
        {
            if (i == str.Length)
            {
                return null;
            }

            return new Data()
            {
                Index = i,
                Value = str[i],
                Next = this.GetData(str, i + 1)
            };
        }

        class Data
        {
            public Data Next { set; get; }

            public int Index { set; get; }

            public char Value { set; get; }
        }
    }
}
