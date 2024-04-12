using MiyGarden.Service.Algorithm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace MiyGarden.Service.Others
{
    public class Zodiac
    {
        public void Main()
        {
            var nums = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            var config2 = GetConfig(nums, 2);
            var config3 = GetConfig(nums, 3);
            var config4 = GetConfig(nums, 4);
            var allConfig = config2.Concat(config3).Concat(config4).ToDictionary(x => x.Key, x => x.Value);
            var config5 = GetConfig2(nums, 2);
            allConfig.Add("*", config5);
            using (var stream = new FileStream(@"D:\ruleconfig.json", FileMode.Create))
            {
                var json = JsonSerializer.Serialize(allConfig, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                var text = Encoding.Unicode.GetBytes(json);
                stream.Write(text, 0, text.Length);
                stream.Close();
            }

            var beton = allConfig.SelectMany(x => x.Value).Select(x => new { x.Name, x.BetTypeBetOn }).OrderBy(x => x.BetTypeBetOn);
            using (var stream = new FileStream(@"D:\combinatoricsbetons.json", FileMode.Create))
            {
                var json = JsonSerializer.Serialize(beton, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                var text = Encoding.Unicode.GetBytes(json);
                stream.Write(text, 0, text.Length);
                stream.Close();
            }
        }

        private Dictionary<string, ZodiacRule[]> GetConfig(List<int> nums, int k)
        {
            var betTypePerMap = new Dictionary<int, string>()
            {
               // EXACTA(FORECAST)
               { 2, "1715"}, 
               // TRIFECTA
               { 3, "1717"}, 
               // SUPERFECTA
               { 4, "1719"}
            };
            var betTypeComMap = new Dictionary<int, string>()
            {
               // QUINELLA
               { 2, "1716"}, 
               // TRIO
               { 3, "1718"}, 
               // FIRST 4
               { 4, "1720"}
            };
            var result1 = new List<List<int>>();
            var currentCombination1 = new List<int>();
            PermuteAndCombination.Permute(nums, k, currentCombination1, result1);
            var result2 = new List<List<int>>();
            var currentCombination2 = new List<int>();
            PermuteAndCombination.FindCombinations(nums.ToArray(), k, 0, currentCombination2, result2);
            var jsonConfig = result2.ToDictionary(x => string.Join(",", x),
                x =>
                {
                    var result = new List<ZodiacRule>
                    {
                        new ZodiacRule { Permutation = false, Sequence = x.ToArray(), BetTypeBetOn = betTypeComMap[k] +  $"{result2.IndexOf(x) + 1:000000}",
                        Name = "com" + k + "," + (result2.IndexOf(x) + 1) + "|" + string.Join(",",x)}
                    };
                    foreach (var re in result1)
                    {
                        if (re.OrderBy(z => z).SequenceEqual(x))
                            result.Add(new ZodiacRule
                            {
                                Permutation = true,
                                Sequence = re.ToArray(),
                                Name = "per" + k + "," + (result1.IndexOf(re) + 1) + "|" + string.Join(",", re),
                                BetTypeBetOn = betTypePerMap[k] + $"{result1.IndexOf(re) + 1:000000}"
                            });
                    }
                    return result.ToArray();
                });
            return jsonConfig;
        }
        private ZodiacRule[] GetConfig2(List<int> nums, int k)
        {
            var betType = "1721";
            var result2 = new List<List<int>>();
            var currentCombination2 = new List<int>();
            PermuteAndCombination.FindCombinations(nums.ToArray(), k, 0, currentCombination2, result2);
            var jsonConfig = result2.Select(x =>
                {
                    var result = new ZodiacRule
                    {
                        Permutation = false,
                        Sequence = x.ToArray(),
                        BetTypeBetOn = betType + $"{result2.IndexOf(x) + 1:000000}",
                        Name = "com" + k + "," + (result2.IndexOf(x) + 1) + "|" + string.Join(",", x)
                    };
                    return result;
                });
            return jsonConfig.ToArray();
        }

        public class ZodiacRule
        {
            public string BetTypeBetOn { get; set; }
            public int[] Sequence { get; set; }
            public string Name { get; set; }
            public bool Permutation { get; set; }
        }
    }
}
