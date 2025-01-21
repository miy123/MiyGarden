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
                        (map[j], map[i]) = (map[i], map[j]);
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

        public void QuickSort2(int[] source, int left, int right)
        {
            if (left < 0 || right < 0 || right <= left) return;
            var pivot = source[left];
            var i1 = left;
            var i2 = right;
            var stop = false;
            do
            {
                for (var i = i1; i < source.Length; i++)
                {
                    if (i >= i2)
                    {
                        stop = true;
                        break;
                    }

                    if (source[i] > pivot)
                    {
                        i1 = i;
                        break;
                    }
                }

                if (!stop)
                {
                    for (var i = i2; i > 0; i--)
                    {
                        if (source[i] < pivot)
                        {
                            i2 = i;
                            break;
                        }
                    }
                    (source[i2], source[i1]) = (source[i1], source[i2]);
                }
            } while (!stop);

            if (i1 == left) (source[i1], source[right]) = (source[right], source[i1]);
            else (source[i1], source[left]) = (source[left], source[i1]);
            if (i1 == i2)
            {
                QuickSort2(source, left, i1 - 1);
                QuickSort2(source, i1, i1);
                QuickSort2(source, i2 + 1, right);
            }
            else
            {
                QuickSort2(source, left, i1 - 1);
                QuickSort2(source, i1, i1);
                QuickSort2(source, i2, right);
            }
        }

        public void QuickSortInPlace(int[] array, int low, int high)
        {
            if (low < high)
            {
                // Partition the array and get the pivot index
                // Choose the last element as the pivot
                int i = low - 1;
                for (int j = low; j < high; j++)
                {
                    if (array[j] <= array[high])
                    {
                        i++;
                        (array[i], array[j]) = (array[j], array[i]);
                    }
                }

                // Place the pivot in its correct position
                (array[i + 1], array[high]) = (array[high], array[i + 1]);
                int pivotIndex = i + 1;

                // Recursively sort elements before and after partition
                QuickSortInPlace(array, low, pivotIndex - 1);
                QuickSortInPlace(array, pivotIndex + 1, high);
            }
        }
    }
}
