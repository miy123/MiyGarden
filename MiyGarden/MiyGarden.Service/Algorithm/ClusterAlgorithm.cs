using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MiyGarden.Service.Algorithm
{
    /// <summary>
    /// 聚合分析演算法
    /// </summary>
    public class ClusterAlgorithm
    {
        /// <summary>
        /// k-means++ 演算法
        /// </summary>
        /// <param name="k">分類簇種子個數</param>
        /// <returns>聚類結果</returns>
        public List<GroupCareer> KMeansPlus(int k = 3)
        {
            var ran = new Random();
            var data = this.GetData();
            int firstSeedIndex = ran.Next(data.Count);
            var seedList = new List<Career>();

            do
            {
                if (seedList.Count == 0)
                    data.ForEach(x => x.CurrentDistance = this.GetDistance(x, data[firstSeedIndex]));
                else
                {
                    data.ForEach(x =>
                    {
                        double minDistance = 9999;

                        // 取最接近之種子之距離
                        seedList.ForEach(y =>
                        {
                            double dintance = this.GetDistance(x, y);
                            if (minDistance > dintance)
                                minDistance = dintance;
                        });

                        x.CurrentDistance = minDistance;
                    });
                }

                void addRandSeed()
                {
                    var randResult = this.GetRandomList(data, 1).First();
                    if (seedList.Contains(randResult))
                        addRandSeed();
                    else
                        seedList.Add(randResult);
                }

                addRandSeed();
                k--;
            } while (k > 0);

            Console.WriteLine("first seed" + string.Join(",", seedList.Select(y => y.Name)));
            return this.KMeans(seedList, data);
        }

        /// <summary>
        /// k-means 演算法
        /// </summary>
        /// <param name="seedList">種子集合</param>
        /// <param name="dataSource">資料源</param>
        /// <returns>聚類結果</returns>
        private List<GroupCareer> KMeans(List<Career> seedList, List<Career> dataSource)
        {
            var groups = seedList.Select(x => new GroupCareer
            {
                Seed = x,
                Careers = new List<Career>()
            }).ToList();
            dataSource.ForEach(x =>
            {
                double minDistance = 999;
                Career seed = new Career();

                // 取最接近之種子之距離
                seedList.ForEach(y =>
                {
                    double distance = this.GetDistance(x, y);
                    if (minDistance > distance)
                    {
                        minDistance = distance;
                        seed = y;
                    }
                });

                var group = groups.FirstOrDefault(z => z.Seed.Attack == seed.Attack
                && z.Seed.Auxiliary == seed.Auxiliary
                && z.Seed.Defend == seed.Defend);
                group.Careers.Add(x);
            });

            bool isNextLoop = false;

            // 選出每個簇的新種子點(聚類後的新中心)
            groups.ForEach(x =>
            {
                var newSeed = new Career()
                {
                    Attack = Math.Round((x.Careers.Sum(y => y.Attack) / x.Careers.Count), 5),
                    Defend = Math.Round((x.Careers.Sum(y => y.Defend) / x.Careers.Count), 5),
                    Auxiliary = Math.Round((x.Careers.Sum(y => y.Auxiliary) / x.Careers.Count), 5)
                };
                if (x.Seed.Attack != newSeed.Attack
                || x.Seed.Auxiliary != newSeed.Auxiliary
                || x.Seed.Defend != newSeed.Defend)
                {
                    x.Seed = newSeed;
                    isNextLoop = true;
                }
            });

            if (isNextLoop)
                return this.KMeans(groups.Select(x => x.Seed).ToList(), dataSource);
            else
                return groups;
        }

        private List<Career> GetData()
        {
            return new List<Career>()
            {
                new Career()
                {
                    Attack = 5,
                    Defend = 3,
                    Auxiliary = 18,
                    Name = "劍純"
                },
                new Career()
                {
                    Attack = 15,
                    Defend = 9,
                    Auxiliary = 12,
                    Name = "氣純"
                },
                new Career()
                {
                    Attack = 15,
                    Defend = 14,
                    Auxiliary = 12,
                    Name = "冰心"
                },
                new Career()
                {
                    Attack = 15,
                    Defend = 13,
                    Auxiliary = 10,
                    Name = "花間"
                },
                new Career()
                {
                    Attack = 20,
                    Defend = 6,
                    Auxiliary = 9,
                    Name = "蓬萊"
                },
                new Career()
                {
                    Attack = 16,
                    Defend = 14,
                    Auxiliary = 7,
                    Name = "焚影"
                },
                new Career()
                {
                    Attack = 5,
                    Defend = 20,
                    Auxiliary = 2,
                    Name = "分山"
                },
                new Career()
                {
                    Attack = 15,
                    Defend = 16,
                    Auxiliary = 11,
                    Name = "毒經"
                },
                new Career()
                {
                    Attack = 5,
                    Defend = 1,
                    Auxiliary = 18,
                    Name = "傲血"
                },
                new Career()
                {
                    Attack = 12,
                    Defend = 9,
                    Auxiliary = 7,
                    Name = "霸刀"
                },
                new Career()
                {
                    Attack = 20,
                    Defend = 14,
                    Auxiliary = 5,
                    Name = "丐幫"
                },
                new Career()
                {
                    Attack = 10,
                    Defend = 11,
                    Auxiliary = 13,
                    Name = "莫問"
                },
                new Career()
                {
                    Attack = 14,
                    Defend = 12,
                    Auxiliary = 6,
                    Name = "驚羽"
                },
                new Career()
                {
                    Attack = 15,
                    Defend = 11,
                    Auxiliary = 10,
                    Name = "天羅"
                },
                new Career()
                {
                    Attack = 15,
                    Defend = 10,
                    Auxiliary = 12,
                    Name = "藏劍"
                },
            };
        }

        /// <summary>
        /// 使用歐幾里得距離取得兩點間距離
        /// </summary>
        /// <param name="item1"></param>
        /// <param name="item2"></param>
        /// <returns></returns>
        private double GetDistance(Career item1, Career item2)
        {
            return Math.Sqrt(Math.Pow(item1.Attack - item2.Attack, 2) + Math.Pow(item1.Auxiliary - item2.Auxiliary, 2) + Math.Pow(item1.Defend - item2.Defend, 2));
        }

        /// <summary>
        /// 带权重的随机
        /// </summary>
        /// <param name="list">原始列表</param>
        /// <param name="count">随机抽取条数</param>
        /// <returns></returns>
        private List<T> GetRandomList<T>(List<T> list, int count) where T : IDistance
        {
            if (list == null || list.Count <= count || count <= 0)
            {
                return list;
            }

            //计算权重总和
            int totalDintance = 0;
            for (int i = 0; i < list.Count; i++)
            {
                totalDintance += (int)list[i].CurrentDistance;
            }

            //随机赋值权重
            Random ran = new Random(GetRandomSeed());  //GetRandomSeed()随机种子，防止快速频繁调用导致随机一样的问题 
            List<KeyValuePair<int, int>> wlist = new List<KeyValuePair<int, int>>();    //第一个int为list下标索引、第一个int为权重排序值
            for (int i = 0; i < list.Count; i++)
            {
                int w = ((int)list[i].CurrentDistance + 1) + ran.Next(0, totalDintance);   // （权重+1） + 从0到（总权重-1）的随机数
                wlist.Add(new KeyValuePair<int, int>(i, w));
            }

            //排序
            wlist.Sort(
              delegate (KeyValuePair<int, int> kvp1, KeyValuePair<int, int> kvp2)
              {
                  return kvp2.Value - kvp1.Value;
              });

            //根据实际情况取排在最前面的几个
            List<T> newList = new List<T>();
            for (int i = 0; i < count; i++)
            {
                T entiy = list[wlist[i].Key];
                newList.Add(entiy);
            }

            //随机法则
            return newList;
        }
        /// <summary>
        /// 随机种子值
        /// </summary>
        /// <returns></returns>
        private static int GetRandomSeed()
        {
            return RandomNumberGenerator.GetInt32(int.MinValue, int.MaxValue);
        }

        /// <summary>
        /// 職業特徵結構
        /// </summary>
        public class Career : IDistance
        {
            /// <summary>
            /// 傷害性
            /// </summary>
            public double Attack { set; get; }

            /// <summary>
            /// 防禦性
            /// </summary>
            public double Defend { set; get; }

            /// <summary>
            /// 輔助性
            /// </summary>
            public double Auxiliary { set; get; }

            public double CurrentDistance { set; get; }

            /// <summary>
            /// 名稱
            /// </summary>
            public string Name { set; get; }
        }

        public class GroupCareer
        {
            public Career Seed { set; get; }

            public List<Career> Careers { set; get; }
        }

        public interface IDistance
        {
            double CurrentDistance { set; get; }
        }
    }
}
