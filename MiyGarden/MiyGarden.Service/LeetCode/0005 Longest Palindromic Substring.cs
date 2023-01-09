using MiyGarden.Service.Interfaces;
using System;

namespace MiyGarden.Service.LeetCode
{
    public class _05_Longest_Palindromic_Substring : ILeetCode
    {
        public int Number => 5;

        public string[] Main()
        {
            var result = new string[]
            {
                this.LongestPalindrome("babad"),
                this.LongestPalindrome("cbbd"),
                this.LongestPalindrome("bb"),
                this.LongestPalindrome("ccc"),
                this.LongestPalindrome("bccb"),
                this.LongestPalindrome("bccc"),
                this.LongestPalindrome("aaaa"),
                this.LongestPalindrome("ccd"),
                this.LongestPalindrome("bananas")
            };
            foreach (var x in result)
                Console.WriteLine(x);
            return result;
        }

        /// <summary>
        /// [自解] 先判斷重複字元並以之為中心，向外擴展判斷是否為回文
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public string LongestPalindrome(string s)
        {
            var maxStartIndex = 0;
            var maxLength = 0;
            for (var i = 0; i < s.Length; i++)
            {
                var tempMinIndexStart = i;
                var tempMaxIndexStart = i;

                // 取得重複字元最後的index
                var repeatEndIndex = this.GetRepeatLastIndex(s, s[i], i);

                // 若有重複字元，則中心點(結束)與主迴圈判斷之字元index設為此重複字元最後之index
                if (repeatEndIndex != i)
                {
                    tempMaxIndexStart = repeatEndIndex;
                    i = repeatEndIndex;
                }

                int cv = 1;

                // 判斷上下限index是否在S字串長度內
                while (tempMinIndexStart - cv >= 0 && tempMaxIndexStart + cv <= s.Length - 1)
                {
                    var tempMinIndex = tempMinIndexStart - cv;
                    var tempMaxIndex = tempMaxIndexStart + cv;

                    // 向外擴展判斷是否為回文
                    if (s[tempMinIndex] == s[tempMaxIndex])
                        cv++;
                    else
                        break;
                }

                var minIndex = tempMinIndexStart - cv + 1;
                var maxIndex = tempMaxIndexStart + cv - 1;
                if (maxIndex - minIndex + 1 > maxLength)
                {
                    maxStartIndex = minIndex;
                    maxLength = maxIndex - minIndex + 1;
                }
            }

            return s.Substring(maxStartIndex, maxLength);
        }

        private int GetRepeatLastIndex(string s, char ch, int index)
        {
            if (index > s.Length - 1)
                return index - 1;
            if (s[index] == ch)
                return this.GetRepeatLastIndex(s, ch, index + 1);
            else
                return index - 1;
        }
    }
}
