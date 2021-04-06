using MiyGarden.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MiyGarden.Service.LeetCode
{
    public class _15_3Sum : ILeetCode
    {
        public int Number => 15;

        public string[] Main()
        {
            this.Show(this.ThreeSum(new int[] { -4, -1, -1, 0, 1, 2 }));
            this.Show(this.ThreeSum2(new int[] { -4, -1, -1, 0, 1, 2 }));
            //this.Show(this.ThreeSum(new int[] { 3, 0, -2, -1, 1, 2 }));
            //this.Show(this.ThreeSum(new int[] { -4, -2, -2, -2, 0, 1, 2, 2, 2, 3, 4, 4, 6, 6 }));
            //this.Show(this.ThreeSum(new int[] { 0, -4, -1, -4, -2, -3, 2 }));
            this.Show(this.ThreeSum(new int[] { -2, 0, 3, -1, 4, 0, 3, 4, 1, 1, 1, -3, -5, 4, 0 }));
            this.Show(this.ThreeSum2(new int[] { -2, 0, 3, -1, 4, 0, 3, 4, 1, 1, 1, -3, -5, 4, 0 }));

            var result = new string[]
            {
            };
            foreach (var x in result)
                Console.WriteLine(x);
            return result;
        }

        public IList<IList<int>> ThreeSum(int[] nums)
        {
            var result = new List<IList<int>>();
            var left = new List<int>();
            var right = new List<int>();
            var hasZero = 0;
            int i;
            // O(n)
            for (i = 0; i < nums.Length; i++)
            {
                if (nums[i] < 0)
                    left.Add(nums[i]);
                else if (nums[i] > 0)
                    right.Add(nums[i]);
                else
                    hasZero++;
            }

            // n = m + k
            // O(mlogm)
            left = left.OrderBy(x => x).ToList();
            // O(klogk)
            right = right.OrderBy(x => x).ToList();

            if (hasZero >= 3) result.Add(new List<int> { 0, 0, 0 });

            int j;
            var hash = new HashSet<string>();
            var fail = new HashSet<int>();

            // O(n^2logn) 
            for (j = 0; j < left.Count; j++)
            {
                var a = left[j];
                int temp;
                int k;
                for (k = 0; k < right.Count; k++)
                {
                    var b = right[k];
                    temp = a + b;
                    if (fail.Contains(temp)) continue;
                    if (temp == 0)
                    {
                        if (hasZero > 0)
                        {
                            if (hash.Add($"{a}0{b}"))
                                result.Add(new List<int> { a, 0, b });
                        }
                        else
                            fail.Add(temp);
                    }
                    else if (temp < 0)
                    {
                        var index = this.BinarySearch(right, -temp);
                        if (index == -1) fail.Add(temp);
                        if (index != -1 && index != k)
                        {
                            var rr = right[index];
                            if (b > rr)
                            {
                                var c = b;
                                b = rr;
                                rr = c;
                            }
                            if (hash.Add($"{a}{b}{rr}"))
                                result.Add(new List<int> { a, b, rr });
                        }
                    }
                    else if (temp > 0)
                    {
                        var index = this.BinarySearch(left, -temp);
                        if (index == -1) fail.Add(temp);
                        if (index != -1 && index != j)
                        {
                            var rr = left[index];
                            var tempa = a;
                            if (tempa > rr)
                            {
                                var c = tempa;
                                tempa = rr;
                                rr = c;
                            }

                            if (hash.Add($"{tempa}{rr}{b}"))
                                result.Add(new List<int> { tempa, rr, b });
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// [網路解]O(n^2)
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public IList<IList<int>> ThreeSum2(int[] nums)
        {
            var result = new List<IList<int>>(); 
            if (nums.Length < 3) return result;
            Array.Sort(nums);
            var hash = new HashSet<string>();
            int i;
            for (i = 0; i < nums.Length - 2; i++)
            {
                var j = i + 1;
                var k = nums.Length - 1;
                while (j < k)
                {
                    if (nums[i] + nums[j] + nums[k] == 0)
                    {
                        if (hash.Add($"{nums[i]}{nums[j]}{nums[k]}"))
                            result.Add(new List<int> { nums[i], nums[j], nums[k] });
                        j++;
                        k--;
                    }
                    else if (nums[i] + nums[j] + nums[k] > 0)
                        k--;
                    else if (nums[i] + nums[j] + nums[k] < 0)
                        j++;
                }
            }

            return result;
        }

        private int BinarySearch(List<int> list, int SearchKey)
        {
            if (list == null || !list.Any()) return -1;
            int left = 0;
            int right = list.Count - 1;
            while (left <= right)
            {
                int mid = (left + right) / 2;
                if (list[mid] == SearchKey)
                {
                    return mid;
                }
                else
                {
                    if (list[mid] < SearchKey)
                    {
                        left = mid + 1;
                    }
                    else
                    {
                        right = mid - 1;
                    }
                }

            }
            return -1;
        }

        private void Show(IList<IList<int>> list)
        {
            Console.WriteLine("------");
            foreach (var item in list)
            {
                Console.WriteLine(string.Join(',', item));
            }
            Console.WriteLine("------");
        }
    }
}
