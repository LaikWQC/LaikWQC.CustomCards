using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace LaikWQC.CustomCards.Model
{
    public static class CustomEnumProperty<T> where T:Enum
    {
        public static ICustomProperty GetEnumProperty(string header, T value, Func<T, string> headerSetter, Action<T> setter)
        {
            var list = Enum.GetValues(typeof(T)).Cast<T>().ToList();
            return new CustomCollectionPropertyMock(new CustomCollectionProperty<T>(header, value, setter, list, headerSetter, ConditionType.NoEmpty, NullElementType.NoElement));
        }
        public static ICustomProperty GetEnumProperty(string header, T value, Action<T> setter)
        {
            var list = Enum.GetValues(typeof(T)).Cast<T>().ToList();
            return new CustomCollectionPropertyMock(new CustomCollectionProperty<T>(header, value, setter, list, x => GetDisplayValue(x), ConditionType.NoEmpty, NullElementType.NoElement));
        }
        public static string GetDisplayValue(T value)
        {
            if (value == null) return "";

            var fieldInfo = value.GetType().GetField(value.ToString());
            if (fieldInfo == null) return value.ToString();

            var descriptionAttributes = fieldInfo.GetCustomAttributes(
                typeof(DisplayAttribute), false).OfType<DisplayAttribute>().ToArray();

            if (descriptionAttributes.Length == 0) return value.ToString();
            return descriptionAttributes[0].Name ?? value.ToString();
        }
    }

}
