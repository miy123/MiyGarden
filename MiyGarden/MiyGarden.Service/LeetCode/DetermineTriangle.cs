using MiyGarden.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiyGarden.Service.LeetCode
{
    public class DetermineTriangles : ILeetCode
    {
        public int Number => 9999;

        public string[] Main()
        {
            var result = new string[]
            {
                DetermineTriangle(0, 1, 3).ToString(),
                DetermineTriangle(1, 1, 3).ToString(),
                DetermineTriangle(3, 4, 5).ToString(),
                DetermineTriangle(3, 3, 3).ToString(),
                DetermineTriangle(4, 4, 5).ToString(),
                DetermineTriangle(5, 5, 5).ToString(),
                DetermineTriangle(10, 10, 13).ToString(),
                GetCombinedData(new List<string>() { "dd", "aaa" })
            };
            foreach (var x in result)
                Console.WriteLine(x);
            return result;
        }

        ////0 when three integers cannot form a triangle(不是三角形)
        ////1 when three integers can form a right triangle(直角三角形)
        ////2 when three integers can form an isosceles triangle(等腰三角形)
        ////3 when three integers can form an equilateral triangle(正三角形)
        ////4 when three integers can form other kind triangle(其他三角形)
        public int DetermineTriangle(int a, int b, int c)
        {
            if (a == 0 || b == 0 || c == 0)
                return 0;
            // or use quicky sort or linq orderby
            var array = new int[3];
            if (b > a)
            {
                var t = a;
                a = b;
                b = t;
            }

            if (c > a)
            {
                var t = a;
                a = c;
                c = t;
            }

            array[0] = a;
            array[1] = Math.Max(b, c);
            array[2] = Math.Min(b, c);
            if (array[0] > array[1] + array[2] || array[0] - array[1] >= array[2] || array[0] - array[2] >= array[1])
                return 0;
            if (array[0] == array[1])
            {
                if (array[0] == array[2])
                    return 3;
                else
                    return 2;
            }
            else
            {
                if (array[0] * array[0] == array[1] * array[1] + array[2] * array[2])
                    return 1;
                else if (array[1] == array[2])
                    return 2;
                else
                    return 4;
            }
        }

        public string GetCombinedData(List<string> data)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string columnName in data)
            {
                if (data[0] != columnName)
                {
                    sb.Append(", ");
                }

                sb.Append($"\"{columnName}\"");
            }

            return sb.ToString();
            return data.Aggregate((a, b) => $"\"{a}\", \"{b}\"");
        }
    }
}
