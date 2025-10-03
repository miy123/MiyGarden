using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace MiyGarden.Service.Linq
{
    public static class MinTest
    {
        public static void Test()
        {
            Console.WriteLine($"SSE: {Sse.IsSupported}");
            Console.WriteLine($"SSE2: {Sse2.IsSupported}");
            Console.WriteLine($"AVX: {Avx.IsSupported}");
            Console.WriteLine($"AVX2: {Avx2.IsSupported}");
            Console.WriteLine($"AVX-512: {Avx512F.IsSupported}");

            var rnd = new Random((int)DateTime.Now.Ticks);
            var list = System.Linq.Enumerable.Range(1, 5000).Select(a => rnd.Next(1, 5000)).ToList();
            for (var i = 0; i < 10; i++)
            {
                Benchmark(() => TestMinForeach(list), "foreach");
                Benchmark(() => TestMin(list), "LINQ Min");
                Benchmark(() => TestMin2(list), "LINQ2 Min");
            }
        }

        private static void TestMinForeach(IEnumerable<int> list)
        {
            var value = int.MaxValue;
            foreach (var item in list)
            {
                value = Math.Min(value, item);
            }
        }
        private static void TestMin(IEnumerable<int> list)
        {
            var r = list.Min();
        }
        private static void TestMin2(IEnumerable<int> list)
        {
            var r = list.MinTest();
        }
        private static void Benchmark(Action f, string label)
        {
            var sw = Stopwatch.StartNew();
            for (var i = 0; i < 10000; i++)
            {
                f();
            }
            sw.Stop();
            Console.WriteLine($"{label}: {sw.ElapsedMilliseconds} ms");
        }
    }


    public static partial class Enumerable
    {
        public static int MinTest(this IEnumerable<int> source) => MinMaxInteger<int, MinCalc<int>>(source);

        public static long MinTest(this IEnumerable<long> source) => MinMaxInteger<long, MinCalc<long>>(source);

        private readonly struct MinCalc<T> : IMinMaxCalc<T> where T : struct, IBinaryInteger<T>
        {
            public static bool Compare(T left, T right) => left < right;
            public static Vector128<T> Compare(Vector128<T> left, Vector128<T> right) => Vector128.Min(left, right);
            public static Vector256<T> Compare(Vector256<T> left, Vector256<T> right) => Vector256.Min(left, right);
            public static Vector512<T> Compare(Vector512<T> left, Vector512<T> right) => Vector512.Min(left, right);
        }

        private interface IMinMaxCalc<T> where T : struct, IBinaryInteger<T>
        {
            public static abstract bool Compare(T left, T right);
            public static abstract Vector128<T> Compare(Vector128<T> left, Vector128<T> right);
            public static abstract Vector256<T> Compare(Vector256<T> left, Vector256<T> right);
            public static abstract Vector512<T> Compare(Vector512<T> left, Vector512<T> right);
        }

        private static T MinMaxInteger<T, TMinMax>(this IEnumerable<T> source)
            where T : struct, IBinaryInteger<T>
            where TMinMax : IMinMaxCalc<T>
        {
            T value;
            
            if (source is List<T> list)
            {
                ReadOnlySpan<T> span = CollectionsMarshal.AsSpan(list);
                if (!Vector128.IsHardwareAccelerated || !Vector128<T>.IsSupported || span.Length < Vector128<T>.Count)
                {
                    value = span[0];
                    for (int i = 1; i < span.Length; i++)
                    {
                        if (TMinMax.Compare(span[i], value))
                        {
                            value = span[i];
                        }
                    }
                }
                else if (!Vector256.IsHardwareAccelerated || !Vector256<T>.IsSupported || span.Length < Vector256<T>.Count)
                {
                    ref T current = ref MemoryMarshal.GetReference(span);
                    ref T lastVectorStart = ref Unsafe.Add(ref current, span.Length - Vector128<T>.Count);

                    Vector128<T> best = Vector128.LoadUnsafe(ref current);
                    current = ref Unsafe.Add(ref current, Vector128<T>.Count);

                    while (Unsafe.IsAddressLessThan(ref current, ref lastVectorStart))
                    {
                        best = TMinMax.Compare(best, Vector128.LoadUnsafe(ref current));
                        current = ref Unsafe.Add(ref current, Vector128<T>.Count);
                    }
                    best = TMinMax.Compare(best, Vector128.LoadUnsafe(ref lastVectorStart));

                    value = best[0];
                    for (int i = 1; i < Vector128<T>.Count; i++)
                    {
                        if (TMinMax.Compare(best[i], value))
                        {
                            value = best[i];
                        }
                    }
                }
                else if (!Vector512.IsHardwareAccelerated || !Vector512<T>.IsSupported || span.Length < Vector512<T>.Count)
                {
                    ref T current = ref MemoryMarshal.GetReference(span);
                    ref T lastVectorStart = ref Unsafe.Add(ref current, span.Length - Vector256<T>.Count); // SIMD 向量指令一次可以處理幾筆資料

                    Vector256<T> best = Vector256.LoadUnsafe(ref current); // 一次載入256bit的資料，int(4byte=32bit)就是8筆資料
                    current = ref Unsafe.Add(ref current, Vector256<T>.Count); // current指向原current位址後移動Vector256<T>.Count個元素的距離

                    while (Unsafe.IsAddressLessThan(ref current, ref lastVectorStart))
                    {
                        best = TMinMax.Compare(best, Vector256.LoadUnsafe(ref current));
                        // 使用Vector256.Min(left, right)來取出最小值的向量
                        // (1f, 5f, 9f, 3f, 7f, 2f, 6f, 0f) Compare (4f, 2f, 8f, 5f, 3f, 9f, 1f, 10f) =>
                        // (1f, 2f, 8f, 3f, 3f, 2f, 1f, 0f)
                        current = ref Unsafe.Add(ref current, Vector256<T>.Count); 
                        // Array本就是連續的記憶體，因此可以藉著Unsafe類別用單純的Add數字來移動記憶體位址，
                    }
                    best = TMinMax.Compare(best, Vector256.LoadUnsafe(ref lastVectorStart));

                    // 從最小的向量中取出最小的單元素
                    value = best[0];
                    for (int i = 1; i < Vector256<T>.Count; i++)
                    {
                        if (TMinMax.Compare(best[i], value))
                        {
                            value = best[i];
                        }
                    }
                }
                else
                {
                    ref T current = ref MemoryMarshal.GetReference(span);
                    ref T lastVectorStart = ref Unsafe.Add(ref current, span.Length - Vector512<T>.Count); 

                    Vector512<T> best = Vector512.LoadUnsafe(ref current);
                    current = ref Unsafe.Add(ref current, Vector512<T>.Count);

                    while (Unsafe.IsAddressLessThan(ref current, ref lastVectorStart))
                    {
                        best = TMinMax.Compare(best, Vector512.LoadUnsafe(ref current));
                        current = ref Unsafe.Add(ref current, Vector512<T>.Count);
                    }
                    best = TMinMax.Compare(best, Vector512.LoadUnsafe(ref lastVectorStart));

                    value = best[0];
                    for (int i = 1; i < Vector512<T>.Count; i++)
                    {
                        if (TMinMax.Compare(best[i], value))
                        {
                            value = best[i];
                        }
                    }
                }
            }
            else
            {
                using IEnumerator<T> e = source.GetEnumerator();
                value = e.Current;
                while (e.MoveNext())
                {
                    T x = e.Current;
                    if (TMinMax.Compare(x, value))
                    {
                        value = x;
                    }
                }
            }

            return value;
        }
    }
}
