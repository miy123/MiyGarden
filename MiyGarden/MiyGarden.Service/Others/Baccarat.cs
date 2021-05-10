using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MiyGarden.Service.Others
{
    public class BaccaratProcess
    {
        public void Main()
        {
            var bac = new Baccarat();
            for (var i = 1; i <= 1; i++)
            {
                bac.ResetDeck();
                var result = bac.GetProbability();
                Console.WriteLine("init," + result.BigProbability.DynamicValue + "," + result.SmallProbability.DynamicValue);
                //Console.WriteLine("BigSmallProbabilityCount:" + result.BigSmallProbabilityCount);
                //Console.WriteLine("SmallProbability:" + result.SmallProbability.DynamicValue);
                //Console.WriteLine("BigProbability:" + result.BigProbability.DynamicValue);

                StreamReader sr = new StreamReader(@$".\Others\BacData\210401-b01-0{i}.json");
                string line = string.Empty;
                while (!sr.EndOfStream)
                {
                    line += sr.ReadLine();
                }
                sr.Close();
                var obj = JsonConvert.DeserializeObject<BacResult>(line);
                foreach (var record in obj.data.records)
                {
                    bac.LossCards(record.result.Where(x => x != null).Select(x => GetPoints((int)x)).ToArray());
                    result = bac.GetProbability();
                    Console.WriteLine(record.gameIDDisplay + "," + result.BigProbability.DynamicValue + "," + result.SmallProbability.DynamicValue);
                    //Console.WriteLine(record.gameIDDisplay);
                    ////Console.WriteLine("BigSmallProbabilityCount:" + result.BigSmallProbabilityCount);
                    //Console.WriteLine("SmallProbability:" + result.SmallProbability.DynamicValue);
                    //Console.WriteLine("BigProbability:" + result.BigProbability.DynamicValue);

                    //Console.WriteLine("allCards:" + bac.allCards + ", cardDecks:" + JsonConvert.SerializeObject(bac.cardDecks));
                }
            }
        }

        private int GetPoints(int result)
        {
            var points = (result - 1) % 13 + 1;
            return points < 10 ? points : 0;
        }

        public class BacResult
        {
            public Data data { get; set; }

            public string msg { get; set; }

            public string code { get; set; }
        }

        public class Data
        {
            public GameResultVO[] records { get; set; }
        }

        public class GameResultVO
        {
            public string gameIDDisplay { get; set; }

            public int gameStatus { get; set; }

            public int?[] result { get; set; }

            public string[] winning { get; set; }
        }
    }

    public class Baccarat
    {
        private const int deckCount = 8;
        private static int[,] bigSmallArray = new int[10, 10]{
            {1,1,1,1,1,1,1,1,0,0},
            {1,1,1,1,1,1,1,1,0,0},
            {1,1,1,1,1,1,1,1,0,0},
            {1,1,1,1,1,1,1,1,0,0},
            {1,1,1,1,1,1,1,1,0,0},
            {1,1,1,1,1,1,1,1,0,0},
            {1,1,1,1,1,1,0,0,0,0},
            {1,1,1,1,1,1,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0}
        };
        public decimal allCards;
        public Dictionary<int, decimal> cardDecks;

        public Baccarat()
        {
            ResetDeck();
        }

        public void ResetDeck()
        {
            allCards = deckCount * 13 * 4m;
            cardDecks = new Dictionary<int, decimal>(){
                { 0, deckCount*4*4 },
                { 1, deckCount*4 },
                { 2, deckCount*4 },
                { 3, deckCount*4 },
                { 4, deckCount*4 },
                { 5, deckCount*4 },
                { 6, deckCount*4 },
                { 7, deckCount*4 },
                { 8, deckCount*4 },
                { 9, deckCount*4 },
            };
        }

        public void LossCards(int[] result)
        {
            foreach (var i in result)
            {
                cardDecks[i]--;
                allCards--;
            }
        }

        public BaccaratProbability GetProbability()
        {
            var bigProbability = 0.0m;
            var smallProbability = 0.0m;
            for (var p1 = 0; p1 < 10; p1++)
            {
                var pcurrent1 = cardDecks[p1] / allCards;
                if (cardDecks[p1] == 0) continue;
                cardDecks[p1]--;
                for (var b1 = 0; b1 < 10; b1++)
                {
                    var bcurrent1 = cardDecks[b1] / (allCards - 1);
                    if (cardDecks[b1] == 0) continue;
                    cardDecks[b1]--;
                    for (var p2 = 0; p2 < 10; p2++)
                    {
                        var pcurrent2 = cardDecks[p2] / (allCards - 2);
                        if (cardDecks[p2] == 0) continue;
                        cardDecks[p2]--;
                        for (var b2 = 0; b2 < 10; b2++)
                        {
                            var bcurrent2 = cardDecks[b2] / (allCards - 3);
                            if (cardDecks[b2] == 0) continue;
                            cardDecks[b2]--;
                            var bk = (b1 + b2) % 10;
                            var pk = (p1 + p2) % 10;
                            //if (bigSmallArray[bk, pk] == 1)
                            //{
                            //    bigProbability += (bcurrent1 * bcurrent2 * pcurrent1 * pcurrent2);
                            //}
                            //else
                            //{
                            //    smallProbability += (bcurrent1 * bcurrent2 * pcurrent1 * pcurrent2);
                            //}
                            if (bk == 8 || bk == 9 || pk == 8 || pk == 9)
                            {
                                smallProbability += (bcurrent1 * bcurrent2 * pcurrent1 * pcurrent2);
                            }
                            else if (pk == 7 || pk == 6)
                            {
                                if (bk <= 5)
                                {
                                    // 閒不補、補莊
                                    for (var b3 = 0; b3 < 10; b3++)
                                    {
                                        var bcurrent3 = cardDecks[b3] / (allCards - 4);
                                        var cbk = (bk + b3) % 10;
                                        bigProbability += (bcurrent1 * bcurrent2 * pcurrent1 * pcurrent2 * bcurrent3);
                                    }
                                }
                                else
                                {
                                    smallProbability += (bcurrent1 * bcurrent2 * pcurrent1 * pcurrent2);
                                }
                            }
                            else if (pk <= 5)
                            {
                                // 閒先補
                                for (var p3 = 0; p3 < 10; p3++)
                                {
                                    var pcurrent3 = cardDecks[p3] / (allCards - 4);
                                    cardDecks[p3]--;
                                    var cpk = (pk + p3) % 10;
                                    if ((p3 == 8 && bk == 3) || ((p3 == 0 || p3 == 1 || p3 == 8 || p3 == 9) && bk == 4) ||
                                        ((p3 == 0 || p3 == 1 || p3 == 2 || p3 == 3 || p3 == 8 || p3 == 9) && bk == 5) ||
                                        ((p3 == 0 || p3 == 1 || p3 == 2 || p3 == 3 || p3 == 4 || p3 == 5 || p3 == 8 || p3 == 9) && bk == 6)
                                        )
                                    {
                                        bigProbability += (bcurrent1 * bcurrent2 * pcurrent1 * pcurrent2 * pcurrent3);
                                    }
                                    else
                                    {
                                        for (var b3 = 0; b3 < 10; b3++)
                                        {
                                            var bcurrent3 = cardDecks[b3] / (allCards - 5);
                                            var cbk = (bk + b3) % 10;
                                            bigProbability += (bcurrent1 * bcurrent2 * pcurrent1 * pcurrent2 * pcurrent3 * bcurrent3);
                                        }
                                    }
                                    cardDecks[p3]++;
                                }
                            }
                            cardDecks[b2]++;
                        }
                        cardDecks[p2]++;
                    }
                    cardDecks[b1]++;
                }
                cardDecks[p1]++;
            }
            return new BaccaratProbability
            {
                BigProbability = new Fortune()
                {
                    ExpectedValue = 0.9565m,
                    Odds = 0.54m,
                    DynamicValue = bigProbability
                },
                SmallProbability = new Fortune()
                {
                    ExpectedValue = 0.9472m,
                    Odds = 1.5m,
                    DynamicValue = smallProbability
                },
            };
        }
    }

    public class BaccaratProbability
    {
        public Fortune BigProbability { get; set; }

        public Fortune SmallProbability { get; set; }

        public decimal BigSmallProbabilityCount => BigProbability.DynamicValue + SmallProbability.DynamicValue;
    }

    public class Fortune
    {
        public decimal ExpectedValue { get; set; }

        public decimal DynamicExpectedValue => DynamicValue * (1 + Odds);

        public decimal Odds { get; set; }

        public decimal DynamicValue { get; set; }
    }
}
