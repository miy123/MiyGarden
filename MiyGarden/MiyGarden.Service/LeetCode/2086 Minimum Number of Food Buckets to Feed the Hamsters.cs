using MiyGarden.Service.Interfaces;

namespace MiyGarden.Service.LeetCode
{
    /// You are given a 0-indexed string hamsters where hamsters[i] is either:
    /// 'H' indicating that there is a hamster at index i, or
    /// '.' indicating that index i is empty.
    ///     You will add some number of food buckets at the empty indices in order to feed the hamsters.A hamster can be fed if there is at least one food bucket to its left or to its right.More formally, a hamster at index i can be fed if you place a food bucket at index i - 1 and/or at index i + 1.
    /// Return the minimum number of food buckets you should place at empty indices to feed all the hamsters or -1 if it is impossible to feed all of them.
    /// Example 1:
    /// Input: hamsters = "H..H"
    /// Output: 2
    /// Explanation: We place two food buckets at indices 1 and 2.
    /// It can be shown that if we place only one food bucket, one of the hamsters will not be fed.
    /// Example 2:
    /// Input: hamsters = ".H.H."
    /// Output: 1
    /// Explanation: We place one food bucket at index 2.
    /// Example 3:
    /// Input: hamsters = ".HHH."
    /// Output: -1
    /// Explanation: If we place a food bucket at every empty index as shown, the hamster at index 2 will not be able to eat.
    /// Constraints:
    /// 1 <= hamsters.length <= 105
    /// hamsters[i] is either'H' or '.'.
    public class _2086_Minimum_Number_of_Food_Buckets_to_Feed_the_Hamsters : ILeetCode
    {
        public int Number => 2086;
        public string[] Main()
        {
            return [
                "3=" + MinimumBuckets(".HH.H.H.H.."),
                "2=" + MinimumBuckets(".H.H.H.."),
                "6=" + MinimumBuckets(".HH.HH.HH.HH..H"),
                "-1=" + MinimumBuckets("HH........"),
                "0=" + MinimumBuckets(".."),
                "0=" + MinimumBuckets("."),
                "-1=" + MinimumBuckets("HH"),
                "-1=" + MinimumBuckets("H"),
                "2=" + MinimumBuckets("H..H"),
                "2=" + MinimumBuckets(".H.H.H.."),
                "-1=" + MinimumBuckets(".HHH.")];
        }
        /// <summary>
        /// 1=可能的補給點,100=確定設立的補給點,-99=倉鼠暫時無補給,0=倉鼠有補給
        /// </summary>
        /// <param name="hamsters"></param>
        /// <returns></returns>
        public int MinimumBuckets(string hamsters)
        {
            var map = new int[hamsters.Length];
            for (var i = 0; i < hamsters.Length; i++)
            {
                if (hamsters[i] == '.')
                {
                    if (i > 0 && map[i - 1] == -99) // H. 左方有無補給的倉鼠且此地為可能的補給點，於是設立補給點
                    {
                        map[i] = 100;
                        map[i - 1] = 0;
                    }
                    else // .. OR .H 左方有有補給的倉鼠或可能的補給點，此地尚無法決定是否設立補給點
                    {
                        map[i] = 1;
                    }
                }
                else
                {
                    if (i > 0 && map[i - 1] == -99) //HH 左方有無補給的倉鼠
                    {
                        if (i > 1 && map[i - 2] == 1) // 且更左方有可能的補給點，在更左方設立補給點
                        {
                            map[i] = -99;
                            map[i - 1] = 0;
                            map[i - 2] = 100;
                        }
                        else // HHH 更左方依舊是倉鼠，死去
                        {
                            return -1;
                        }
                    }
                    else if (i > 0 && map[i - 1] == 100) // OH 左方為補給點，此地倉鼠獲得補給
                    {
                        map[i] = 0;
                    }
                    else if (i > 0 && map[i - 1] == 1) // 左方為可能的補給點
                    {
                        if (i > 1 && map[i - 2] == -99) // 且更左方為無補給的倉鼠，左方設立補給點，此處倉鼠亦獲得補給
                        {
                            map[i] = 0;
                            map[i - 2] = 0;
                            map[i - 1] = 100;
                        }
                        else // 且更左方為有補給的倉鼠或補給點，因此與此處無關，此處倉鼠尚未獲得補給
                        {
                            map[i] = -99;
                        }
                    }
                    else // 左方為有補給的倉鼠，因此與此處無關，此處倉鼠尚未獲得補給
                    {
                        map[i] = -99;
                    }
                }
            }

            var count = 0;
            for (var i = 0; i < map.Length; i++)
            {
                if (map[i] == 100) count++;
                if (map[i] == -99)
                {
                    if (((i > 0 && map[i - 1] == 1) || (i < map.Length - 1 && map[i + 1] == 1))) count++;
                    else return -1;
                }
            }
            return count;
        }
    }
}