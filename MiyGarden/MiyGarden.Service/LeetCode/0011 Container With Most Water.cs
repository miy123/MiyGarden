using MiyGarden.Service.Interfaces;
using System;

namespace MiyGarden.Service.LeetCode
{
    public class _11_Container_With_Most_Water : ILeetCode
    {
        public int Number => 11;

        public string[] Main()
        {
            var result = new string[]
            {
                "49=" + this.MaxArea(new int[] { 1, 8, 6, 2, 5, 4, 8, 3, 7 })
            };
            foreach (var x in result)
                Console.WriteLine(x);
            return result;
        }

        /// <summary>
        /// [網路解] 漸進縮小
        /// https://leetcode.com/problems/container-with-most-water/discuss/6099/yet-another-way-to-see-what-happens-in-the-on-algorithm
        /// </summary>
        /// <param name="height"></param>
        /// <returns></returns>
        public int MaxArea(int[] height)
        {
            var maxarea = 0;
            var l = 0;
            var r = height.Length - 1;
            while (l < r)
            {
                // 若移動長柱則新面積永遠<=現在的面積
                // 若有l<r，則面積為l*(r-l)且必>=面積(l+n)*(r-(l+n))，不管長柱如何移動必小於等於原本面積
                // 故移動短柱
                maxarea = Math.Max(maxarea, Math.Min(height[l], height[r]) * (r - l));
                if (height[l] < height[r])
                    l++;
                else
                    r--;
            }

            return maxarea;
        }

        /// <summary>
        /// [自解] 迴圈遍歷
        /// </summary>
        /// <param name="height"></param>
        /// <returns></returns>
        public int MaxArea2(int[] height)
        {
            var max = 0;
            for (var i = 0; i < height.Length; i++)
            {
                for (var j = i + 1; j < height.Length; j++)
                {
                    var area = Math.Min(height[i], height[j]) * Math.Abs(i - j);
                    if (area > max)
                        max = area;
                }
            }

            return max;
        }
    }
}
