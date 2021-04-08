using MiyGarden.Service.Interfaces;
using System;

namespace MiyGarden.Service.LeetCode
{
    public class _0121_Best_Time_to_Buy_and_Sell_Stock : ILeetCode
    {
        public int Number => 121;

        public string[] Main()
        {
            var result = new string[]
            {
                "5=" + MaxProfit(new int[] { 7, 1, 5, 3, 6, 4 }),
                "0=" + MaxProfit(new int[] { 7, 6, 4, 3, 1 }),
                "4=" + MaxProfit(new int[] { 7, 5, 3, 6, 4, 1, 5 })
            };
            foreach (var x in result)
                Console.WriteLine(x);
            return result;
        }

        public int MaxProfit(int[] prices)
        {
            if (prices.Length == 0) return 0;
            var value = 0;
            var min = prices[0];
            var max = min;
            int i;
            for (i = 1; i < prices.Length; i++)
            {
                if (prices[i] > min)
                {
                    if (prices[i] > max)
                    {
                        max = prices[i];
                        value = Math.Max(prices[i] - min, value);
                    }
                }
                else
                {
                    min = prices[i];
                    max = min;
                }
            }
            return value;
        }
    }
}
