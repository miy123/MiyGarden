using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Linq;

namespace MiyGarden.Service.Extensions
{
    public static class EnumExtension
    {
        static readonly ConcurrentDictionary<Enum, string> _descriptionCache = new ConcurrentDictionary<Enum, string>();

        public static string GetDescription(this Enum @enum)
        {
            if (!_descriptionCache.ContainsKey(@enum))
            {
                var fieldInfo = @enum.GetType().GetField(@enum.ToString());
                var attribute = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false).Cast<DescriptionAttribute>().FirstOrDefault();
                _descriptionCache[@enum] = attribute?.Description ?? @enum.ToString();
            }
            return _descriptionCache[@enum];
        }

        public static string GetDescription2(this Enum sourceEnum)
        {
            Type type = sourceEnum.GetType();

            var name = Enum.GetNames(type)
                            .Where(f => f.Equals(sourceEnum.ToString(), StringComparison.CurrentCultureIgnoreCase))
                            .Select(d => d)
                            .FirstOrDefault();

            // 利用反射找出相對應的欄位
            var field = type.GetField(name);
            // 取得欄位設定DescriptionAttribute的值
            var customAttribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false);

            // 無設定Description Attribute, 回傳Enum欄位名稱
            if (customAttribute == null || customAttribute.Length == 0)
            {
                return name;
            }

            // 回傳Description Attribute的設定
            return ((DescriptionAttribute)customAttribute[0]).Description;
        }
    }
}
