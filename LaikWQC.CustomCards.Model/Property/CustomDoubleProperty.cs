using System;
using System.Globalization;
using System.Linq;

namespace LaikWQC.CustomCards.Model
{
    public class CustomDoubleProperty : CustomProperty<double?>, ICustomProperty
    {
        public CustomDoubleProperty(string header, double? value, Action<double?> setter, Func<double?, bool> correctCondition = null)
            : base(header, value, setter, correctCondition) 
        {
            Setup();
        }

        public CustomDoubleProperty(string header, double? value, Action<double?> setter, ConditionType conditionType)
            : base(header, value, setter, conditionType) 
        {
            Setup();
        }

        private void Setup()
        {
            SynchronizeValues();
            ValueChanged += SynchronizeValues;
        }

        public string ValueStr
        {
            get => _valueStr;
            set
            {
                if (string.IsNullOrEmpty(value)) { Value = null; return; }

                var separator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0];
                var correctValue = value.Replace(',', separator).Replace('.', separator); //меняем запятые и точки на сепаратор из нашей культуры
                if (correctValue.Count(x => x == separator) > 1) return; //не позволяем создать 2 сепаратора
                if (correctValue.StartsWith(separator)) correctValue = "0" + correctValue; //если начинается с сепаратора, то добавляем 0 вначале

                _valueStr = correctValue;                

                if (double.TryParse(_valueStr, NumberStyles.Any, CultureInfo.CurrentCulture, out var newValue))
                {
                    Value = newValue; 
                    if (correctValue.EndsWith(separator)) //в случае, когда удаляем последную цифру после запятой, в конце остается сепаратор - после срабатывания ивента и метода SynchronizeValues() я снова устанавливаю значение с сепаратором(correctValue)
                        _valueStr = correctValue;
                }
                    
                else SynchronizeValues();
            }
        }
        private string _valueStr;

        private void SynchronizeValues()
        {
            _valueStr = Value.ToString();
        }
    }
}
