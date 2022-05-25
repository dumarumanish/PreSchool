using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

namespace PreSchool.Application
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
                        ShortName = displayAttribute?.ShortName,
                        GroupName = displayAttribute?.GroupName,

                        DisplayOrder = (displayAttribute?.GetOrder() ?? 0) == 0 ? Convert.ToInt32(enumValue) : displayAttribute.Order,
                    });
            }
            return totalEnum;
        }

        public static string ToNameString<T>(this T enumValue) where T : Enum
        {
            Type type = enumValue.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException("Enumeration Value must be of Enum type", "enumerationValue");
            }

            //Tries to find a DescriptionAttribute for a potential friendly name
            //for the enum
            MemberInfo[] memberInfo = type.GetMember(enumValue.ToString());
            if (memberInfo != null && memberInfo.Length > 0)
            {

                var displayAttribute = memberInfo[0].GetCustomAttribute<DisplayAttribute>();

                return displayAttribute?.Name ?? enumValue.ToString();
            }
            //If we have no description attribute, just return the ToString of the enum
            return enumValue.ToString();
        }
    }
    public class EnumValue
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
    }
}
