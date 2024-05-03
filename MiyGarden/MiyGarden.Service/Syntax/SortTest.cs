using MiyGarden.Models.Models;
using MiyGarden.Service.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MiyGarden.Service.Syntax
{
    public class SortTest
    {
        private static List<Person> List = new List<Person> {
                        new Person
                        {
                            Name = "文天祥",
                            Dynasty = "宋朝"
                        },
                        new Person
                        {
                            Name = "張世傑",
                            Dynasty = "宋朝"
                        },
                        new Person
                        {
                            Name = "文天祥",
                            Dynasty = "南宋"
                        }};

        public void Test()
        {
            //var str = "{\"Name\":\"clark\",\"MediaType\":2,\"MediaType123\":2}";
            //var model = JsonConvert.DeserializeObject<Person>(str);
            List.Sort((x, y) => x.Name == "張世傑" ? 1 : -1);
            Show(List.Distinct(x => x.Name));
            var list = new List<int> { 1, 2, 3, 1, 5, 4, 4, 1 };
            list.Sort((x, y) =>
            {
                if (x == y) return 0;
                else if (x > y) return 1;
                else return -1;
            });
            Show(list);
        }

        private static void Show<T>(IEnumerable<T> lists)
        {
            void _Show()
            {
                foreach (var item in lists)
                    Console.WriteLine(item);
                Console.WriteLine("----");
            }
            _Show();
        }
    }
}
