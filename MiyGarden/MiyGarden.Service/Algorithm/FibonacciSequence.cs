using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiyGarden.Service.Algorithm
{
    /// <summary>
    /// 費式數列
    /// </summary>
    public class FibonacciSequence
    {
        /// <summary>
        /// 遞迴解
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public int GetNumber(int n)
        {
            if (n == 1)
                return 1;

            if (n > 1)
                return this.GetNumber(n - 1) + this.GetNumber(n - 2);
            else
                return 0;
        }
    }
}
