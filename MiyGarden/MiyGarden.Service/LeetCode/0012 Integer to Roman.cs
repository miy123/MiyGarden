using MiyGarden.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiyGarden.Service.LeetCode
{
    public class _12_Integer_to_Roman : ILeetCode
    {
        public int Number => 12;

        private readonly Dictionary<int, string> maps = new Dictionary<int, string>()
        {
            { 1 , "I"},
            { 5 , "V"},
            { 10 , "X"},
            { 50 , "L"},
            { 100 , "C"},
            { 500 , "D"},
            { 1000 , "M"},
        };

        private readonly int[] indexs = { 1, 5, 10, 50, 100, 500, 1000 };

        public string[] Main()
        {
            var result = new string[]
            {
                "III=" + this.IntToRoman(3),
                "IV=" + this.IntToRoman(4),
                "IX=" + this.IntToRoman(9),
                "LVIII=" + this.IntToRoman(58),
                "MCMXCIV=" + this.IntToRoman(1994)
            };
            foreach (var x in result)
                Console.WriteLine(x);
            return result;
        }

        public string IntToRoman(int num)
        {
            var result = new StringBuilder();
            var tn = num;
            int i;
            for (i = this.indexs.Length - 1; i >= 0; i--)
            {
                var index = this.indexs[i];
                if (index > tn) continue;
                var intValue = tn / index;
                if (intValue != 0)
                {
                    var firstNum = this.GetFirstNum(tn);
                    if (index == 1000 || (firstNum != 9 && firstNum != 4))
                    {
                        if (firstNum == 1)
                            result.Append(maps[index]);
                        else
                            for (var j = 0; j < intValue; j++)
                                result.Append(maps[index]);
                        tn %= index;
                    }
                    else
                    {
                        if (firstNum == 9)
                        {
                            result.Append(this.maps[this.indexs[i - 1]]);
                            result.Append(this.maps[this.indexs[i + 1]]);
                            tn -= this.indexs[i - 1] * 9;
                        }
                        else
                        {
                            result.Append(this.maps[this.indexs[i]]);
                            result.Append(this.maps[this.indexs[i + 1]]);
                            tn -= this.indexs[i] * 4;
                        }
                    }
                }

                if (tn == 0) break;
            }

            return result.ToString();
        }

        private int GetFirstNum(int num)
        {
            var value = num / 10;
            if (value > 0)
                return GetFirstNum(value);
            return num;
        }
    }
}
