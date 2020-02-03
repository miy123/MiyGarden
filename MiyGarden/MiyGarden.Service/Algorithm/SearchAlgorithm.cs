using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiyGarden.Service.Algorithm
{
    public class SearchAlgorithm
    {
        /// <summary>
        /// 二元搜尋法
        /// </summary>
        /// <param name="searchKey">尋找之值</param>
        /// <param name="arr">有序陣列</param>
        public void BinarySearch(int searchKey = 104, int[] arr = null)
        {
            if (arr == null) arr = new int[] { 1, 2, 4, 8, 10, 22, 50, 55, 60, 100, 104, 150 };
            int start = 0, end = arr.Length, mid;
            while (start <= end)
            {
                mid = (start + end) / 2;
                if (arr[mid] < searchKey)
                {
                    Console.WriteLine("start:" + start + ",end:" + end + ",mid:" + mid);
                    start = mid + 1;
                }
                else if (arr[mid] > searchKey)
                {
                    Console.WriteLine("start:" + start + ",end:" + end + ",mid:" + mid);
                    end = mid - 1;
                }
                else
                {
                    Console.WriteLine("start:" + start + ",end:" + end + ",result:" + mid);
                    break;
                }
            }
        }
    }
}
