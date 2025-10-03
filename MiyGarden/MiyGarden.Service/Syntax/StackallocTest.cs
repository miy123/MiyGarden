using System;
using System.Diagnostics;

namespace MiyGarden.Service.Syntax
{
    public static class StackallocTest
    {
        public static void Test()
        {
            var st = new Stopwatch();
            st.Start();
            Span<int> tmp = stackalloc int[100];
            for (int i = 0; i < tmp.Length; i++)
            {
                tmp[i] = i;
            }

            for (int i = 0; i < tmp.Length; i++)
            {
                Console.WriteLine(tmp[i]);
            }

            st.Stop();
            Console.WriteLine("stackalloc int[] ElapsedMilliseconds:" + st.ElapsedMilliseconds.ToString());

            st.Restart();
            int[] tmp2 = new int[100];
            for (int i = 0; i < tmp2.Length; i++)
            {
                tmp2[i] = i;
            }

            for (int i = 0; i < tmp2.Length; i++)
            {
                Console.WriteLine(tmp2[i]);
            }

            st.Stop();
            Console.WriteLine("int[] ElapsedMilliseconds:" + st.ElapsedMilliseconds.ToString());

            st.Restart();
            int[] tmp3 = new int[100];
            for (int i = 0; i < tmp3.Length; i++)
            {
                tmp3[i] = i;
            }

            var tmp4 = tmp3.AsSpan();
            for (int i = 0; i < tmp4.Length; i++)
            {
                Console.WriteLine(tmp4[i]);
            }

            st.Stop();
            Console.WriteLine("int[] AsSpan ElapsedMilliseconds:" + st.ElapsedMilliseconds.ToString());
        }
    }
}
