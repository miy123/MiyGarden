using MiyGarden.Service.Interfaces;
using System;

namespace MiyGarden.Service.LeetCode
{
    // Constraints: 1 <= n <= 45
    public class _0070_Climbing_Stairs : ILeetCode
    {
        public int Number => 70;

        public string[] Main()
        {
            Console.WriteLine("2 = " + ClimbStairs(2));
            Console.WriteLine("3 = " + ClimbStairs(3));
            Console.WriteLine("5 = " + ClimbStairs(4));
            Console.WriteLine("8 = " + ClimbStairs(5));

            Console.WriteLine("14930352 = " + ClimbStairs(35));
            Console.WriteLine("24157817 = " + ClimbStairs(36));
            Console.WriteLine("39088169 = " + ClimbStairs(37));
            Console.WriteLine("63245986 = " + ClimbStairs(38));
            Console.WriteLine("102334155 = " + ClimbStairs(39));
            Console.WriteLine("165580141 = " + ClimbStairs(40));
            Console.WriteLine("267914296 = " + ClimbStairs(41));
            Console.WriteLine("433494437 = " + ClimbStairs(42));
            Console.WriteLine("701408733 = " + ClimbStairs(43));
            Console.WriteLine("1134903170 = " + ClimbStairs(44));

            var result = Array.Empty<string>();
            foreach (var x in result)
                Console.WriteLine(x);
            return result;
        }

        // 先找有幾總1,2數字組合(不管位置；1,2跟2,1算一種)，再根據每種組合列出所有排列(管位置；1,2跟2,1算兩種)
        // n=2
        // 1 + 1

        // 2

        // n=3
        // 1 + 1 + 1

        // 1 + 2
        // 2 + 1
        // C2取1 1 * 2 = 2

        // n=4
        // 1+1+1+1

        // 1+1+2
        // C3取1 1 * 2 * 3 / 1 * 2 = 3

        // 2+2
        // C2取2 1

        // n=5
        // 1+1+1+1+1

        // 1+1+1+2
        // 1+2+1+1
        // 2+1+1+1
        // 1+2+1+1
        // C4取1 1 * 2 * 3 * 4 / 1 * 1 * 2 * 3 = 4

        // 1+2+2
        // 2+1+2
        // 2+2+1
        // C3取2 1 * 2 * 3 / 1 * 2 * 1 = 3

        public int ClimbStairs(int n)
        {
            if (n == 1) return 1;
            if (n == 2) return 2;
            var isEven = n % 2 == 0;
            var twoCount = isEven ? n / 2 : (n - 1) / 2;
            var count = 1; // 1+1+..
            for (var i = 1; i <= twoCount; i++)
            {
                var remainingCount = n - 2 * i;
                // C (remainingCount + i) 取 i
                var combineCount = Combine(remainingCount + i, i);
                count += combineCount;
            }
            return count;
        }

        // C m 取 n
        private int Combine(int m, int n)
        {
            double temp = 1;
            // 保證n>=m-n
            if (n < m - n)
                return Combine(m, m - n);
            for (var i = 0; i < m - n; ++i)
            {
                temp *= n + i + 1;
                temp /= i + 1;
            }
            return (int)temp;
        }
    }
}