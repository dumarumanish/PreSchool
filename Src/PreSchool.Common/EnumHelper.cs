using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

namespace eCommerce.Common.HelperClasses
{
    public static class EnumHelper
    {
        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, ignoreCase: true);
        }
        public static List<EnumValue> GetEnumValues<T>() where T : Enum
        {
            var totalEnum = new List<EnumValue>();
            foreach (var enumName in Enum.GetNames(typeof(T)))
            {
                var member = typeof(T).GetMember(enumName);
                //If there is no DisplayAttribute then the Enum is not used
                var displayAttribute = member[0].GetCustomAttribute<DisplayAttribute>();
                //if (displayAttribute == null)
                //    continue;
                var enumValue = EnumHelper.ParseEnum<T>(enumName);
                totalEnum.Add(
                    new EnumValue
                    {
                        Id = Convert.ToInt32(enumValue),
                        Name = enumName,
                        Description = displayAttribute?.Description ?? enumName,
                        DisplayName = displayAttribute?.Name ?? enumName,
                        SortOrder = (displayAttribute?.GetOrder() ?? 0) == 0 ? Convert.ToInt32(enumValue) : displayAttribute.Order,
                    });
            }
            return totalEnum;
        }
    }
    public class EnumValue
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public int SortOrder { get; set; }
    }
}
