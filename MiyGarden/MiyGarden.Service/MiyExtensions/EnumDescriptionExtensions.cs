using System;
using System.ComponentModel;
using System.Linq;

namespace MiyGarden.Service.MiyExtensions
{
    public static class EnumDescriptionExtensions
    {
        public static string GetDescription(this Enum sourceEnum)
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