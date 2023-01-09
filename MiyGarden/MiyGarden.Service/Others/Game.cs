using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiyGarden.Service.Others
{
    public class Game
    {
        private int[] _mysteryNumber;

        public void StartGame()
        {
            Console.Write("輸入4個0~9數字，並以逗號隔開(1,2,3,4)");
            string myNumbers = Console.ReadLine();
            var numbers = transStringToArry(myNumbers);
            if (numbers != null && numbers.Count() == 4)
                this.SetMySteryNumber(numbers);
            else
                this.StartGame();
        }

        public void StartRound()
        {
            var result = GetMagicNumber();
            Console.WriteLine(result.Item1);
            if (result.Item2)
                this.StopGame();
            else
                this.StartRound();
        }

        private void StopGame()
        {
            Console.WriteLine("結束遊戲");
        }

        private void SetMySteryNumber(int[] mysteryNumber)
        {
            this._mysteryNumber = mysteryNumber;
            Console.Clear();
            Console.WriteLine("設置成功.");
        }

        private readonly Func<string, int[]> transStringToArry = n =>
        {
            string myNumbers = Console.ReadLine();
            List<int> setNumbers = new List<int>();
            if (!string.IsNullOrEmpty(myNumbers))
            {
                string[] source = myNumbers.Split(',');
                foreach (var item in source)
                {
                    if (int.TryParse(item, out int result))
                        setNumbers.Add(result);
                }
                if (setNumbers.Count != 4)
                    Console.WriteLine("格式不合.");
            }
            else
            {
                Console.WriteLine("不得為空.");
            }
            return setNumbers.ToArray();
        };

        private Tuple<string, bool> GetMagicNumber()
        {
            if (this._mysteryNumber == null || this._mysteryNumber.Length <= 0)
                this.StartGame();

            Console.Write("輸入猜測：");
            string myNumbers = Console.ReadLine();
            var resultString = Tuple.Create("", false);
            var numbers = transStringToArry(myNumbers);
            if (numbers != null && numbers.Count() == 4)
                resultString = this.ProcessMagicNumber(numbers);
            return resultString;
        }

        private Tuple<string, bool> ProcessMagicNumber(int[] numbers)
        {
            if (numbers == null)
                throw new ArgumentNullException(nameof(numbers));

            int countA = 0;
            int countB = 0;

            for (var i = 0; i < numbers.Length; i++)
            {
                var temp = numbers[i];
                if (_mysteryNumber[i] == temp)
                {
                    countA++;
                }
                else if (_mysteryNumber.Contains(temp))
                {
                    countB++;
                }
            }
            return Tuple.Create(countA + "A" + countB + "B", countA == 4);
        }
    }
}
