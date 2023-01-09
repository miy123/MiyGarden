using System.Collections.Generic;
using System.Linq;

namespace MiyGarden.Service.Algorithm
{
    public class SortAlgorithm
    {
        public int[] QuickSort(int[] source)
        {
            if (source.Length <= 1) return source;
            var left = new List<int>(source.Length);
            var right = new List<int>(source.Length);
            var pivot = source[source.Length / 2];
            var pivotList = new List<int>(1);
            foreach (var item in source)
            {
                if (item < pivot)
                    left.Add(item);
                else if (item > pivot)
                    right.Add(item);
            }

            pivotList.Add(pivot);

            return QuickSort(left.ToArray()).Concat(pivotList).Concat(QuickSort(right.ToArray())).ToArray();
        }

        /// <summary>
        /// linq order by
        /// </summary>
        /// <param name="map"></param>
        /// <param name="left">0</param>
        /// <param name="right">map.length-1</param>
        public void QuickSort(int[] map, int left, int right)
        {
            do
            {
                int i = left;
                int j = right;
                int x = map[i + ((j - i) >> 1)];
                do
                {
                    while (i < map.Length/* && CompareKeys(x, map[i]) > 0*/) i++;
                    while (j >= 0 /*&& CompareKeys(x, map[j]) < 0*/) j--;
                    if (i > j) break;
                    if (i < j)
                    {
                        int temp = map[i];
                        map[i] = map[j];
                        map[j] = temp;
                    }
                    i++;
                    j--;
                } while (i <= j);
                if (j - left <= right - i)
                {
                    if (left < j) QuickSort(map, left, j);
                    left = i;
                }
                else
                {
                    if (i < right) QuickSort(map, i, right);
                    right = j;
                }
            } while (left < right);
        }

        //private int CompareKeys(int index1, int index2)
        //{
        //    int c = comparer.Compare(keys[index1], keys[index2]);
        //    if (c == 0)
        //    {
        //        if (next == null) return index1 - index2;
        //        return next.CompareKeys(index1, index2);
        //    }
        //    return descending ? -c : c;
        //}
    }
}
