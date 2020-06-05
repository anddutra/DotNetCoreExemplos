using System;
using System.Linq;
using System.Reflection;

namespace FireBaseExemplos.Util
{
    public static class ExtractAttributeEnum
    {
        internal static T? GetEnumFromValue<T>(object value) where T : struct
        {
            var enumTypeInfo = typeof(T).GetFields();
            var listEnumMembers = enumTypeInfo.Where(x =>
            {
                var attribute = x.GetCustomAttribute<EnumValueAttribute>();
                if (attribute != null)
                {
                    return attribute.Value == (string)value;
                }
                return false;
            });

            if (listEnumMembers == null || !listEnumMembers.Any())
                return null;
            else
            {
                string enumElementName = listEnumMembers.FirstOrDefault().Name;
                return (T)Enum.Parse(typeof(T), enumElementName);
            }

        }

        internal static string GetEnumValue<T>(T value)
        {
            var enumTypeInfo = typeof(T).GetField(value.ToString());
            var attribute = enumTypeInfo.GetCustomAttribute<EnumValueAttribute>();
            if (attribute != null)
            {
                return attribute.Value;
            }
            else
                return null;
        }
    }
}
