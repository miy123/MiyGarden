using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiyGarden.Service.Others
{
    public class TicketCheckHelper
    {
        /// <summary>
        /// 取得檢查碼
        /// </summary>
        /// <param name="ticket">准考證號碼</param>
        /// <returns></returns>
        public int GetCheckNumber(string ticket)
        {
            int result = 0;
            string digitString = GetDigitString(ticket);
            for (var i = 0; i < digitString.Length; i++)
            {
                char c = digitString[i];
                if (char.IsDigit(c))
                    result += GetProduct(i + 1, (int)char.GetNumericValue(c), digitString.Length);
            }
            return result % 10 == 0 ? 0 : 10 - result % 10;
        }

        /// <summary>
        /// 轉碼後string(10位)
        /// </summary>
        /// <param name="orginString"></param>
        /// <returns></returns>
        private string GetDigitString(string orginString)
        {
            string digitString = string.Empty;
            for (var i = 0; i < orginString.Length; i++)
            {
                char c = orginString[i];
                if (char.IsLetter(c))
                    digitString += TransLetterToDigit(c);
                else if (char.IsDigit(c))
                    digitString += c;
            }
            return digitString.Substring(0, 10);
        }

        private int TransLetterToDigit(char c)
        {
            int result = 0;
            switch (c)
            {
                case 'A':
                    result = 10;
                    break;
                case 'B':
                    result = 11;
                    break;
                case 'C':
                    result = 12;
                    break;
                case 'D':
                    result = 13;
                    break;
                case 'E':
                    result = 14;
                    break;
                case 'F':
                    result = 15;
                    break;
                case 'G':
                    result = 16;
                    break;
                case 'H':
                    result = 17;
                    break;
                case 'I':
                    result = 34;
                    break;
                case 'J':
                    result = 18;
                    break;
                case 'K':
                    result = 19;
                    break;
                case 'L':
                    result = 20;
                    break;
                case 'M':
                    result = 21;
                    break;
                case 'N':
                    result = 22;
                    break;
                case 'O':
                    result = 35;
                    break;
                case 'P':
                    result = 23;
                    break;
                case 'Q':
                    result = 24;
                    break;
                case 'R':
                    result = 25;
                    break;
                case 'S':
                    result = 26;
                    break;
                case 'T':
                    result = 27;
                    break;
                case 'U':
                    result = 28;
                    break;
                case 'V':
                    result = 29;
                    break;
                case 'W':
                    result = 40;
                    break;
                case 'X':
                    result = 30;
                    break;
                case 'Y':
                    result = 31;
                    break;
                case 'Z':
                    result = 41;
                    break;
                default:
                    break;
            }
            return result;
        }

        private int GetProduct(int index, int value, int length)
        {
            if (index == 1 || index == length) return value * 1;
            return (length - index + 1) * value;
        }
    }
}
