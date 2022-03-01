using System;
using System.Collections.Generic;
using System.Linq;

namespace LaikWQC.CustomCards.Model
{
    public class CustomCollectionProperty<T> : CustomProperty<T>, ICollectionProperty
    {
        public CustomCollectionProperty(string header, T value, Action<T> setter, ICollection<T> collection, Func<T, string> headerSetter, ConditionType conditionType, NullElementType nullElementType, string nullElementHeader = "<Не выбрано>") : base(header, value, setter)
        {
            switch (conditionType)
            {
                case ConditionType.NoCondition:
                    _correctCondition = null;
                    break;
                case ConditionType.NoEmpty:
                    _correctCondition = x => x != null;
                    break;
            }

            _collection = new List<CustomHeaderedItem<T>>();
            switch (nullElementType)
            {
                case NullElementType.MockElement:
                    //Если T может быть null, то добавляем элемент-заглушку для null значения
                    if ((T)default == null)
                        _collection.Add(new CustomHeaderedItem<T>() { Header = nullElementHeader, Value = default });
                    break;
            }
            _collection.AddRange(collection.Select(x => new CustomHeaderedItem<T>() { Header = headerSetter(x), Value = x }));

            var selected = _collection.FirstOrDefault(x => x.Value?.Equals(Value) ?? false);
            if (selected == null)
            {
                //Если value нет в предоставленной коллекции, то приравняем его к 1-му элементу 
                Value = _collection[0].Value;
                _selectedItemIndex = 0;
            }
            else
                _selectedItemIndex = _collection.IndexOf(selected);
        }

        private List<CustomHeaderedItem<T>> _collection;
        private int _selectedItemIndex;

        public ICollection<object> GetCollection()
        {
            return _collection.Select(x => (object)x).ToList();
        }

        public void SetValue(object value)
        {
            var item = value as CustomHeaderedItem<T>;
            Value = item == null ? default : item.Value;
        }

        public int SelectedIndex => _selectedItemIndex;
    }
}
