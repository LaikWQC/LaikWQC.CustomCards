using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace LaikWQC.CustomCards.Model
{
    public abstract class CustomProperty<T> : ICustomPropertyBase, INotifyPropertyChanged
    {
        protected Func<T, bool> _correctCondition;
        protected Action<T> _setter;
        private readonly IEqualityComparer<T> _equalityComparer;
        private T _initialValue;

        public CustomProperty(string header, T value, Action<T> setter, Func<T, bool> correctCondition, IEqualityComparer<T> equalityComparer = null)
            : this(header, value, setter, equalityComparer)
        {
            _correctCondition = correctCondition;
        }
        public CustomProperty(string header, T value, Action<T> setter, ConditionType condition, IEqualityComparer<T> equalityComparer = null)
            :this(header, value, setter, equalityComparer)
        {
            switch (condition)
            {
                case ConditionType.NoCondition:
                    _correctCondition = null;
                    break;
                case ConditionType.NoEmpty:
                    _correctCondition = x => !x.Equals(default);
                    break;
            }
        }
        public CustomProperty(string header, T value, Action<T> setter, IEqualityComparer<T> equalityComparer = null)
        {
            Header = header;
            _initialValue = value;
            _value = value;
            _setter = setter;
            _equalityComparer = equalityComparer ?? EqualityComparer<T>.Default;
        }

        public string Header { get; }

        public event Action ValueChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        public T Value
        {
            get => _value;
            set
            {
                if (_equalityComparer.Equals(_value, value)) return;
                _value = value;
                ValueChanged?.Invoke();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
            }
        }
        private T _value;

        public bool IsCorrected => _correctCondition?.Invoke(_value) ?? true;

        public void ConfirmChanges() => _setter?.Invoke(Value);

        public void Reset()
        {
            Value = _initialValue;
        }
    }

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
        }
    }
}
