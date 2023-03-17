using System;
using System.Collections.Generic;

namespace LaikWQC.CustomCards.Model
{
    public static class CustomProperty
    {
        public static ICustomProperty CreateStringProperty(string header, string value, Action<string> setter, Func<string, bool> correctCondition = null)
        => new CustomStringProperty(header, value, setter, correctCondition);
        public static ICustomProperty CreateStringProperty(string header, string value, Action<string> setter, ConditionType conditionType)
            => new CustomStringProperty(header, value, setter, conditionType);

        public static ICustomProperty CreateIntProperty(string header, int? value, Action<int?> setter, Func<int?, bool> correctCondition = null)
            => new CustomIntProperty(header, value, setter, correctCondition);
        public static ICustomProperty CreateIntProperty(string header, int? value, Action<int?> setter, ConditionType conditionType)
            => new CustomIntProperty(header, value, setter, conditionType);

        public static ICustomProperty CreateDoubleProperty(string header, double? value, Action<double?> setter, Func<double?, bool> correctCondition = null)
            => new CustomDoubleProperty(header, value, setter, correctCondition);
        public static ICustomProperty CreateDoubleProperty(string header, double? value, Action<double?> setter, ConditionType conditionType)
            => new CustomDoubleProperty(header, value, setter, conditionType);

        public static ICustomProperty CreateTimeProperty(string header, DateTime? value, Action<DateTime?> setter, Func<DateTime?, bool> correctCondition = null)
            => new CustomDateProperty(header, value, setter, correctCondition);
        public static ICustomProperty CreateDateProperty(string header, DateTime? value, Action<DateTime?> setter, ConditionType conditionType)
            => new CustomDateProperty(header, value, setter, conditionType);

        public static ICustomProperty CreateCollectionProperty(ICollectionProperty property)
            => new CustomCollectionPropertyMock(property);

        /// <summary>
        /// Создает комбобокс с null элементом (если T не может быть null, то заглушка не создается!)
        /// </summary>
        /// <param name="headerSetter">Как отображать элементы</param>
        /// <param name="conditionType">Можно запретить выбирать null елемент</param>
        /// <param name="nullElementHeader">Заголовок null елемента</param>
        /// <returns></returns>
        public static ICustomProperty CreateCollectionProperty<T>(string header, T value, Action<T> setter, ICollection<T> collection, Func<T, string> headerSetter, ConditionType conditionType, string nullElementHeader = "<Не выбрано>")
            => new CustomCollectionPropertyMock(new CustomCollectionProperty<T>(header, value, setter, collection, headerSetter, conditionType, NullElementType.MockElement, nullElementHeader));
        
        /// <summary>
        /// Создает комбобокс без возможности выбора null элемента 
        /// </summary>
        /// <param name="headerSetter">Как отображать элементы</param>
        /// <returns></returns>
        public static ICustomProperty CreateCollectionProperty<T>(string header, T value, Action<T> setter, ICollection<T> collection, Func<T, string> headerSetter)
            => new CustomCollectionPropertyMock(new CustomCollectionProperty<T>(header, value, setter, collection, headerSetter, ConditionType.NoCondition, NullElementType.NoElement));

        /// <summary>
        /// Создает комбобокс из всех возможных значений енума value. 
        /// Наименование значений берутся из атрибута Display.Name
        /// </summary>
        public static ICustomProperty CreateEnumProperty<T>(string header, T value, Action<T> setter) where T: Enum
            => CustomEnumProperty<T>.GetEnumProperty(header, value, setter);

        /// <summary>
        /// Создает комбобокс из всех возможных значений енума value.
        /// </summary>
        /// <param name="headerSetter">Как отображать элементы</param>
        public static ICustomProperty CreateEnumProperty<T>(string header, T value, Func<T,string> headerSetter, Action<T> setter) where T : Enum
            => CustomEnumProperty<T>.GetEnumProperty(header, value, headerSetter, setter);

        public static class Extra
        {
            /// <summary>
            /// Вставляет разделитель в виде пустой строки
            /// </summary>
            public static ICustomProperty CreateSeparator() => new CustomSeparatorProperty();
            public static ICustomProperty CreateExpander(string header, bool isExpanded, ICollection<ICustomProperty> properties)
                => new CustomExpanderProperty(header, isExpanded, properties);
            public static ICustomProperty CreateGroup(string header, ICollection<ICustomProperty> properties)
                => new CustomGroupProperty(header, properties);
        }
    }
}
