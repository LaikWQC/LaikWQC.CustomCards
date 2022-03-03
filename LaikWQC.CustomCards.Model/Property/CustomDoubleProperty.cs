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
            _valueStr = Value.ToString();
        }

        public CustomDoubleProperty(string header, double? value, Action<double?> setter, ConditionType conditionType)
            : base(header, value, setter, conditionType)
        {
            _valueStr = Value.ToString();
        }

        public string ValueStr
        {
            get => _valueStr;
            set
            {
                if (string.IsNullOrEmpty(value)) { Value = null; _valueStr = ""; return; }

                var separator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0];
                var correctValue = value.Replace(',', separator).Replace('.', separator); //меняем запятые и точки на сепаратор из нашей культуры
                if (correctValue.Count(x => x == separator) > 1) return; //не позволяем создать 2 сепаратора
                if (correctValue.StartsWith(separator)) correctValue = "0" + correctValue; //если начинается с сепаратора, то добавляем 0 в начало

                if (double.TryParse(correctValue, NumberStyles.Any, CultureInfo.CurrentCulture, out var newValue))
                {
                    Value = newValue;
                    _valueStr = correctValue;
                }
                else _valueStr = Value.ToString();
            }
        }
        private string _valueStr;
    }
}
