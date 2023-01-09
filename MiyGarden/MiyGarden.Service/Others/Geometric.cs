using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiyGarden.Service.Others
{
    public class Geometric
    {
        /// <summary>
        /// 菱形
        /// </summary>
        /// <param name="n">深度</param>
        public void Rin(int n)
        {
            int i, j;
            for (i = 0; i < n; i++)
            {
                if (i <= n / 2)
                {
                    for (j = 0; j < (n / 2) - i; j++)
                    {
                        Console.Write(" ");
                    }
                    for (j = 0; j < i * 2 + 1; j++)
                    {
                        Console.Write("*");
                    }

                    Console.WriteLine();
                }
                else
                {
                    for (j = 0; j < i - (n / 2); j++)
                    {
                        Console.Write(" ");
                    }
                    for (j = 0; j < (n - i) * 2 - 1; j++)
                    {
                        Console.Write("*");
                    }
                    Console.WriteLine();
                }
            }
        }

        /// <summary>
        /// 菱形
        /// </summary>
        /// <param name="number">深度</param>
        public void Rin2(int number)
        {
            for (int j = 1; j <= number; j++)
            {
                string output = ""; int i;
                if (j <= (number + 1) / 2) i = j * 2 - 1;
                else i = (-1 * (j - number) + 1) * 2 - 1;
                var space = (number - i);
                for (int k = 1; k <= number; k++)
                {
                    if (k > space / 2 && k <= number - space / 2) output = output + "*";
                    else output = output + " ";
                }
                Console.WriteLine(output);
            }
        }
    }
}
