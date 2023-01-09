using MiyGarden.Models.Models;
using MiyGarden.Service.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            List.Sort((x, y) => x.Name == "張世傑" ? 1 : -1);
            Show(List.Distinct(x => x.Name));
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
