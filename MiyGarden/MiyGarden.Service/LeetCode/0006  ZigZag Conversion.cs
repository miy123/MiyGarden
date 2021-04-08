using MiyGarden.Service.Interfaces;
using System;
using System.Text;

namespace MiyGarden.Service.LeetCode
{
    public class _06__ZigZag_Conversion : ILeetCode
    {
        public int Number => 6;

        public string[] Main()
        {
            var result = new string[]
            {
                "PAHNAPLSIIGYIR=" + this.Convert("PAYPALISHIRING", 3),
                "PINALSIGYAHRPI=" + this.Convert("PAYPALISHIRING", 4)
            };
            foreach (var x in result)
                Console.WriteLine(x);
            return result;
        }

        /// <summary>
        /// [自解] 先以numRows算出多少數值為一組，再以row為單位一行一行算出char之index
        /// </summary>
        /// <param name="s"></param>
        /// <param name="numRows"></param>
        /// <returns></returns>
        public string Convert(string s, int numRows)
        {
            var group = this.GetGroup(numRows);
            var result = new StringBuilder();
            for (var deep = 1; deep <= numRows; deep++)
            {
                if (deep == 1 || deep == numRows)
                {
                    for (var index = deep - 1; index < s.Length; index += group)
                        result.Append(s[index]);
                }
                else
                {
                    for (var index = deep - 1; index < s.Length; index += group)
                    {
                        if (index < s.Length)
                            result.Append(s[index]);
                        else
                            break;
                        var iv = index - (deep - 1) + group - (deep - 1);
                        if (iv < s.Length)
                            result.Append(s[iv]);
                        else
                            break;
                    }
                }
            }

            return result.ToString();
        }

        private int GetGroup(int i)
        {
            if (i < 3)
                return i;

            var result = 0;
            for (var j = 1; j <= i - 2; j++)
                result++;

            return i + result;
        }
    }
}
