using MiyGarden.Service.Interfaces;
using System;

namespace MiyGarden.Service.LeetCode
{
    public class _07_Reverse_Integer : ILeetCode
    {
        public int Number => 7;

        public void Main()
        {
            Console.WriteLine(this.Reverse(-123));
        }

        public int Reverse(int x)
        {
            long result = 0;
            while (x != 0)
            {
                result = result * 10 + x % 10;
                x /= 10;
            }

            return result > int.MaxValue || result < int.MinValue ? 0 : (int)result;
        }
    }
}
