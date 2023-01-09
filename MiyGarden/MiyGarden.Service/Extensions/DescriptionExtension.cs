using MiyGarden.Models.Attributes;
using System;
using System.Collections.Generic;

namespace MiyGarden.Service.Extensions
{
    public static class DescriptionExtension
    {
        public static IEnumerable<string> GetDescription<T>()
        {
            var members = typeof(T).GetMembers();
            foreach (var item in members)
            {
                var attributes = item.GetCustomAttributes(typeof(MyDescriptionAttribute), false);
                if (attributes.Length > 0)
                {
                    var description = ((MyDescriptionAttribute)attributes[0])?.Description;
                    yield return description ?? string.Empty;
                }
            }
        }
    }
}
