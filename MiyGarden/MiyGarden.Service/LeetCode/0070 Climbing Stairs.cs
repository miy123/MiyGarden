using MiyGarden.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiyGarden.Service.LeetCode
{
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

            Console.WriteLine("2 = " + ClimbStairs2(2));
            Console.WriteLine("3 = " + ClimbStairs2(3));
            Console.WriteLine("5 = " + ClimbStairs2(4));
            Console.WriteLine("8 = " + ClimbStairs2(5));

            Console.WriteLine("14930352 = " + ClimbStairs2(35));
            Console.WriteLine("24157817 = " + ClimbStairs2(36));
            Console.WriteLine("39088169 = " + ClimbStairs2(37));
            Console.WriteLine("63245986 = " + ClimbStairs2(38));
            Console.WriteLine("102334155 = " + ClimbStairs2(39));
            Console.WriteLine("165580141 = " + ClimbStairs2(40));
            Console.WriteLine("267914296 = " + ClimbStairs2(41));
            Console.WriteLine("433494437 = " + ClimbStairs2(42));
            Console.WriteLine("701408733 = " + ClimbStairs2(43));
            Console.WriteLine("1134903170 = " + ClimbStairs2(44));

            var result = new string[]
            {
            };
            foreach (var x in result)
                Console.WriteLine(x);
            return result;
        }

        public int ClimbStairs(int n)
        {
            int i;
            var count = 1;
            for (i = 1; i <= n / 2; i++)
            {
                // i + n - i*2 => n - i
                var temp = Permutations(n - i, i);
                count += temp;
            }
            return count;
        }

        private int Permutations(int index, int seed)
        {
            var molecular1 = seed;
            var molecular2 = index - seed;
            if (molecular1 < molecular2)
            {
                var temp = molecular1;
                molecular1 = molecular2;
                molecular2 = temp;
            }
            int i;
            ulong result = 1;
            var dividend = (ulong)GetClass(molecular2);
            for (i = molecular1 + 1; i <= index; i++)
            {
                if (result % dividend == 0)
                {
                    result /= dividend;
                    dividend = 1;
                }
                if (result % 2 == 0 && dividend % 2 == 0)
                {
                    result /= 2;
                    dividend /= 2;
                }
                result *= (ulong)i;
            }

            result /= dividend;
            return (int)result;
        }

        private int GetClass(int num)
        {
            if (num == 0) return 1;
            int i;
            var result = 1;
            for (i = 2; i <= num; i++)
                result *= i;
            return result;
        }

        public int ClimbStairs2(int n)
        {
            return climb_Stairs(0, n);
        }
        private int climb_Stairs(int i, int n)
        {
            if (i > n)
            {
                return 0;
            }
            if (i == n)
            {
                return 1;
            }
            return climb_Stairs(i + 1, n) + climb_Stairs(i + 2, n);
        }
    }
}
