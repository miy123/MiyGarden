using MiyGarden.Service.Extensions;
using MiyGarden.Service.Interfaces;
using System;
using System.Linq;
using System.Reflection;

namespace MiyGarden.LeetCode
{
    static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("enter the number of leetcode topic");
            var numberString = Console.ReadLine();
            var leetCode = Assembly.GetExecutingAssembly()
                .GetReferencedAssemblyList()
                .SelectMany(x => x.GetExportedTypes().Where(y => typeof(ILeetCode).IsAssignableFrom(y) && !y.IsInterface))
                .Select(p => (ILeetCode)Activator.CreateInstance(p))
                .FirstOrDefault(x => x.Number == short.Parse(numberString));
            if (leetCode != null) leetCode.Main();
            else Console.WriteLine("尚無此題號");
        }
    }
}
