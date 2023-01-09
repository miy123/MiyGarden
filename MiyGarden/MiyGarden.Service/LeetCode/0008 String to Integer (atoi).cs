using MiyGarden.Service.Interfaces;
using System;

namespace MiyGarden.Service.LeetCode
{
    public class _08_String_to_Integer : ILeetCode
    {
        public int Number => 8;

        public string[] Main()
        {
            var result = new string[]
            {
                this.MyAtoi("-123").ToString(),
                this.MyAtoi("   -42").ToString(),
                this.MyAtoi("4193 with words").ToString(),
                this.MyAtoi("words and 987").ToString(),
                this.MyAtoi("-91283472332").ToString(),
                this.MyAtoi("").ToString(),
                this.MyAtoi("+").ToString(),
                this.MyAtoi("   +0 123").ToString(),
                this.MyAtoi(" ").ToString()
            };
            foreach (var x in result)
                Console.WriteLine(x);
            return result;
        }

        public int MyAtoi(string str)
        {
            long result;
            bool flag = false;
            do
            {
                if (str.Length < 1)
                    break;
                if (str[0] == ' ' || str[0] == '　')
                    str = str.Remove(0, 1);
                else
                    flag = true;
            } while (!flag);
            if (string.IsNullOrEmpty(str))
                return 0;

            if (str[0] == '+')
                result = this.ParseInt(str, 1, 1);
            else if (str[0] == '-')
                result = -1 * this.ParseInt(str, 1, 1);
            else
                result = this.ParseInt(str, 0, 1);

            if (result > int.MaxValue)
                result = int.MaxValue;
            if (result < int.MinValue)
                result = int.MinValue;

            return (int)result;
        }

        private long ParseInt(string s, int startIndex, int length)
        {
            if (startIndex == s.Length)
                return 0;
            if (long.TryParse(s.Substring(startIndex, length), out _))
                return s.Length - startIndex > length ? ParseInt(s, startIndex, length + 1) : long.Parse(s.Substring(startIndex, length));
            else
                return length == 1 ? 0 : long.Parse(s.Substring(startIndex, length - 1));
        }
    }
}
