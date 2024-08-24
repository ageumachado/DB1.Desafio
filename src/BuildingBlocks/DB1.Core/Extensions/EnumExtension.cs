using System.ComponentModel;
using System.Globalization;

namespace DB1.Core.Extensions
{
    public static class EnumExtension
    {
        public static string GetDescription<T>(this T e) where T : IConvertible
        {
            if (e is not Enum)
            {
                return string.Empty;
            }

            Type type = e.GetType();
            Array values = Enum.GetValues(type);

            foreach (int val in values)
            {
                if (val == e.ToInt32(CultureInfo.InvariantCulture))
                {
                    var enumName = type.GetEnumName(val)!;
                    var memInfo = type.GetMember(enumName);

                    if (memInfo[0]
                        .GetCustomAttributes(typeof(DescriptionAttribute), false)
                        .FirstOrDefault() is DescriptionAttribute descriptionAttribute)
                    {
                        return descriptionAttribute.Description;
                    }

                    return enumName;
                }
            }

            return string.Empty;
        }
    }
}
