using MiyGarden.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiyGarden.Service.LeetCode
{
    public class _04_Median_of_Two_Sorted_Arrays : ILeetCode
    {
        public int Number => 4;

        public string[] Main()
        {
            var result = new string[]
            {
                "2=" + this.FindMedianSortedArrays(new int[] { 1, 3 }, new int[] { 2 }),
                "2.5=" + this.FindMedianSortedArrays(new int[] { 1, 2 }, new int[] { 3, 4 }),
                "1=" + this.FindMedianSortedArrays(new int[] { }, new int[] { 1 }),
                "1.5=" + this.FindMedianSortedArrays(new int[] { 1, 2 }, new int[] { -1, 3 }),
                "-1=" + this.FindMedianSortedArrays(new int[] { 3 }, new int[] { -2, -1 }),
                "1.5=" + this.FindMedianSortedArrays(new int[] { 1, 2 }, new int[] { 1, 2 }),
                "2.5=" + this.FindMedianSortedArrays(new int[] { 3 }, new int[] { 1, 2, 4 }),
                "3=" + this.FindMedianSortedArrays(new int[] { 1, 2 }, new int[] { 3, 4, 5 }),
                "2=" + this.FindMedianSortedArrays(new int[] { 1, 2, 3 }, new int[] { 1, 2, 2 }),
                "2=" + this.FindMedianSortedArrays(new int[] { 1, 1, 3, 3 }, new int[] { 1, 1, 3, 3 })
            };
            foreach (var x in result)
                Console.WriteLine(x);
            return result;
        }

        public double FindMedianSortedArrays(int[] A, int[] B)
        {
            int m = A.Length;
            int n = B.Length;
            if (m > n)
            {   // to ensure m<=n
                int[] temp = A; A = B; B = temp;
                int tmp = m; m = n; n = tmp;
            }
            int iMin = 0, iMax = m, halfLen = (m + n + 1) / 2;
            while (iMin <= iMax)
            {
                int i = (iMin + iMax) / 2;
                int j = halfLen - i;
                if (i < iMax && B[j - 1] > A[i])
                {
                    iMin = i + 1; // i is too small
                }
                else if (i > iMin && A[i - 1] > B[j])
                {
                    iMax = i - 1; // i is too big
                }
                else
                {   // i is perfect
                    int maxLeft;
                    if (i == 0) { maxLeft = B[j - 1]; }
                    else if (j == 0) { maxLeft = A[i - 1]; }
                    else { maxLeft = Math.Max(A[i - 1], B[j - 1]); }
                    if ((m + n) % 2 == 1) { return maxLeft; }

                    int minRight;
                    if (i == m) { minRight = B[j]; }
                    else if (j == n) { minRight = A[i]; }
                    else { minRight = Math.Min(B[j], A[i]); }

                    return (maxLeft + minRight) / 2.0;
                }
            }
            return 0.0;
        }
    }
}
