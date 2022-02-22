using System;

namespace LaikWQC.CustomCards.Model
{
    public class CustomDoubleProperty : CustomProperty<double?>, ICustomProperty
    {
        public CustomDoubleProperty(string header, double? value, Action<double?> setter, Func<double?, bool> correctCondition = null) : base(header, setter)
        {
            Value = value;
            _correctCondition = correctCondition;
        }

        public CustomDoubleProperty(string header, double? value, Action<double?> setter, ConditionType conditionType) : base(header, setter)
        {
            Value = value;
            switch (conditionType)
            {
                case ConditionType.NoCondition:
                    _correctCondition = null;
                    break;
                case ConditionType.NoEmpty:
                    _correctCondition = x => x != null;
                    break;
            }
        }
    }
}
