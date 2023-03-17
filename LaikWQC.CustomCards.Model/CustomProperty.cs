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
}
